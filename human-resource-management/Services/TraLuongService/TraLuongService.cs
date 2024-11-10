using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Adapter.Update_DotTraLuong;
using HumanResourceManagement.Pattern.Singleton;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.TraLuongService
{
    public class TraLuongService : ITraLuongService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<TraLuong> uniqueIdGenerator;
        private readonly TraLuongProfile traLuongProfile;
        private readonly IUpdateDotTraLuongTarget updateDotTraLuongTarget;

        public TraLuongService(HumanResourceManagementDbContext dbContext, IUniqueIdGenerator<TraLuong> uniqueIdGenerator, TraLuongProfile traLuongProfile, IUpdateDotTraLuongTarget updateDotTraLuongTarget)
        {
            _dbContext = dbContext;
            this.uniqueIdGenerator = uniqueIdGenerator;
            this.traLuongProfile = traLuongProfile;
            this.updateDotTraLuongTarget = updateDotTraLuongTarget;
        }

        public async Task<ICollection<object>> GetTraLuongs()
        {
            ICollection<TraLuong> traLuongs = await _dbContext.TraLuongs
                .Include(t => t.NhanVien)
                .Include(t => t.DotTraLuong)
                .ToListAsync();

            return traLuongProfile.MapToListDto(traLuongs);
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10, string employeeName = "", DateTime? month = null)
        {
            // Lấy tổng số lượng mục
            var totalCount = _dbContext.TraLuongs.Count();
            var query = _dbContext.TraLuongs.AsQueryable();

            if(month.HasValue)
            {
                query = query.Where(dtl
                   => dtl.NgayTraLuong.Month.Equals(month.Value.Month)
                   && dtl.NgayTraLuong.Year.Equals(month.Value.Year)
               );
            }

            if(!string.IsNullOrEmpty(employeeName))
            {
                query = query
                    .Include(tl => tl.NhanVien)
                    .Where(tl => tl.NhanVien.HoTen.ToLower().Contains(employeeName.ToLower()));
            }

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            // Sử dụng Skip() và Take() để phân trang
            var items = query
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Include(h => h.NhanVien)
                                .Include(o => o.DotTraLuong)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = traLuongProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<ICollection<object>> GetTraLuongByMaNhanVien(string maNhanVien)
        {
            List<TraLuong> traLuongs = await _dbContext.TraLuongs
               .Include(tl => tl.NhanVien)
               .Include(tl => tl.DotTraLuong)
               .Where(dt => dt.NhanVienId.Equals(maNhanVien)).ToListAsync();

            return traLuongProfile.MapToListDto(traLuongs);
        }

        public async Task<object> ThemTraLuong(TraLuongDTO traLuongDTO)
        {
            DotTraLuong? dotTraLuong = await _dbContext.DotTraLuongs
                .SingleOrDefaultAsync(nv => nv.MaDotTraLuong.Equals(traLuongDTO.DotTraLuongId))
                    ?? throw new NotFoundException("Không tìm đợt trả lương");

            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(traLuongDTO.NhanVienId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            TraLuong? checkTraLuong = await _dbContext.TraLuongs
                .SingleOrDefaultAsync(tl => tl.DotTraLuongId.Equals(dotTraLuong.MaDotTraLuong) && tl.NhanVienId.Equals(nhanVien.MaNhanVien));

            if (checkTraLuong == null)
            {
                Luong? luongNhanVien = await _dbContext.Luongs
                .SingleOrDefaultAsync(lg => lg.ThangTinhLuong == dotTraLuong.ThangDotTraLuong
                && lg.NamTinhLuong == dotTraLuong.NamDotTraLuong
                && lg.NhanVienId == nhanVien.MaNhanVien);

                TraLuong traLuong = new TraLuong()
                {
                    MaTraLuong = await uniqueIdGenerator.GetUniqueID("TL", 8, "MaTraLuong"),
                    GhiChu = traLuongDTO.GhiChu,
                    NgayTraLuong = traLuongDTO.NgayTraLuong,
                    NhanVien = nhanVien,
                    DotTraLuong = dotTraLuong
                };

                if (luongNhanVien != null)
                {
                    if (luongNhanVien.ThuNhapRong - traLuongDTO.SoTienDaTra >= 0)
                    {
                        traLuong.SoTienCanPhaiTra = luongNhanVien.ThuNhapRong;
                        traLuong.SoTienDaTra = traLuongDTO.SoTienDaTra;
                        traLuong.SoTienConPhaiTra = luongNhanVien.ThuNhapRong - traLuongDTO.SoTienDaTra;
                        if (Math.Ceiling(traLuong.SoTienConPhaiTra) == 0 || Math.Floor(traLuong.SoTienConPhaiTra) == 0)
                        {
                            traLuong.TrangThai = TrangThaiTraLuong.DaTraLuong;
                        }
                        else
                        {
                            traLuong.TrangThai = TrangThaiTraLuong.DangNoLuong;
                        }
                    }
                    else
                    {
                        throw new ConflictException("Số tiền cần phải trả không được nhiều hơn tiền lương");
                    }
                }
                else
                {
                    throw new ConflictException("Phải tính lương cho nhân viên này trước khi trả lương");
                }

                await _dbContext.TraLuongs.AddAsync(traLuong);
                await _dbContext.SaveChangesAsync();
                await updateDotTraLuongTarget.UpdateDotTraLuong(dotTraLuong);
                //await dotTraLuongSingleton.UpdateDotTraLuong(dotTraLuong);
                //await DotTraLuongSingleton.GetInstance().UpdateDotTraLuong(_dbContext, dotTraLuong);

                return traLuongProfile.MapToDto(traLuong);
            }
            else if (checkTraLuong != null && checkTraLuong.TrangThai.Equals(TrangThaiTraLuong.DangNoLuong))
            {
                if(traLuongDTO.SoTienDaTra > checkTraLuong.SoTienConPhaiTra)
                {
                    throw new ConflictException("Số tiền cần phải trả không được nhiều hơn tiền lương");
                }
                checkTraLuong.SoTienDaTra += traLuongDTO.SoTienDaTra;
                checkTraLuong.SoTienConPhaiTra -= traLuongDTO.SoTienDaTra;

                if (Math.Ceiling(checkTraLuong.SoTienConPhaiTra) == 0 || Math.Floor(checkTraLuong.SoTienConPhaiTra) == 0)
                {
                    checkTraLuong.TrangThai = TrangThaiTraLuong.DaTraLuong;
                }

                await _dbContext.SaveChangesAsync();
                await updateDotTraLuongTarget.UpdateDotTraLuong(dotTraLuong);

            }

            return traLuongProfile.MapToDto(checkTraLuong);

        }

        public async Task<bool> CapNhatTraLuong(string id, TraLuongDTO traLuongDTO)
        {
            NhanVien? nhanVien = await _dbContext.NhanViens
               .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(traLuongDTO.NhanVienId))
                   ?? throw new NotFoundException("Không tìm thấy nhân viên");

            DotTraLuong? dotTraLuong = await _dbContext.DotTraLuongs
                .SingleOrDefaultAsync(nv => nv.MaDotTraLuong.Equals(traLuongDTO.DotTraLuongId))
                    ?? throw new NotFoundException("Không tìm đợt trả lương");

            TraLuong? traLuong = await _dbContext.TraLuongs
               .Include(t => t.NhanVien)
               .Include(t => t.DotTraLuong)
               .SingleOrDefaultAsync(dt => dt.MaTraLuong.Equals(id))
                   ?? throw new NotFoundException("Không tìm thấy thông tin trả lương");

            TraLuong? existedTraLuong = await _dbContext.TraLuongs.SingleOrDefaultAsync(tl => tl.DotTraLuongId.Equals(dotTraLuong.MaDotTraLuong) && tl.NhanVienId.Equals(nhanVien.MaNhanVien));
            if (existedTraLuong != null)
            {
                if (!existedTraLuong.MaTraLuong.Equals(traLuong.MaTraLuong))
                {
                    throw new ConflictException("Trả lương của nhân viên này đã có");
                }
            }
            Luong? luongNhanVien = await _dbContext.Luongs
            .SingleOrDefaultAsync(lg => lg.ThangTinhLuong == dotTraLuong.ThangDotTraLuong
            && lg.NamTinhLuong == dotTraLuong.NamDotTraLuong
            && lg.NhanVienId == nhanVien.MaNhanVien);

            traLuong.GhiChu = traLuongDTO.GhiChu;
            traLuong.NgayTraLuong = traLuongDTO.NgayTraLuong;
            traLuong.NhanVien = nhanVien;
            traLuong.DotTraLuong = dotTraLuong;

            if (luongNhanVien != null)
            {
                if (!traLuong.TrangThai.Equals(TrangThaiTraLuong.DaTraLuong))
                {
                    double soTienDaTra = traLuong.SoTienDaTra + traLuongDTO.SoTienDaTra;

                    if (luongNhanVien.ThuNhapRong - soTienDaTra >= 0)
                    {
                        traLuong.SoTienCanPhaiTra = luongNhanVien.ThuNhapRong;
                        traLuong.SoTienDaTra += traLuongDTO.SoTienDaTra;
                        traLuong.SoTienConPhaiTra = luongNhanVien.ThuNhapRong - soTienDaTra;
                        if (traLuong.SoTienConPhaiTra == 0)
                        {
                            traLuong.TrangThai = TrangThaiTraLuong.DaTraLuong;
                        }
                        else
                        {
                            traLuong.TrangThai = TrangThaiTraLuong.DangNoLuong;
                        }
                        await _dbContext.SaveChangesAsync();
                        await updateDotTraLuongTarget.UpdateDotTraLuong(dotTraLuong);

                    }
                    else
                    {
                        throw new ConflictException("Số tiền cần phải trả không được nhiều hơn tiền lương");
                    }
                }
                else if (traLuong.TrangThai.Equals(TrangThaiTraLuong.DaTraLuong) && traLuongDTO.SoTienCanPhaiTra > traLuong.SoTienCanPhaiTra)
                {
                    traLuong.SoTienCanPhaiTra = luongNhanVien.ThuNhapRong;
                    traLuong.SoTienConPhaiTra = luongNhanVien.ThuNhapRong - traLuong.SoTienDaTra;
                    traLuong.TrangThai = TrangThaiTraLuong.DangNoLuong;
                    await _dbContext.SaveChangesAsync();
                    await updateDotTraLuongTarget.UpdateDotTraLuong(dotTraLuong);

                }
                else
                {
                    throw new ConflictException("Lương của nhân viên này đã được trả đủ");
                }
            }
            else
            {
                throw new ConflictException("Phải tính lương cho nhân viên này trước khi trả lương");
            }
            return true;
        }

        public async Task<object> GetTraLuongById(string dotTraLuongId, string maNhanVien)
        {
            TraLuong? checkTraLuong = await _dbContext.TraLuongs
                 .SingleOrDefaultAsync(tl => tl.DotTraLuongId.Equals(dotTraLuongId) && tl.NhanVienId.Equals(maNhanVien));

            if (checkTraLuong == null)
                throw new ConflictException("Không tìm thấy thông tin trả lương");

            return traLuongProfile.MapToDto(checkTraLuong);
        }
    }
}
