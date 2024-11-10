using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Builder;
using HumanResourceManagement.Pattern.Builder.NghiVangBuild;
using HumanResourceManagement.Pattern.Observer.ChamCongObserver;
using HumanResourceManagement.Pattern.State;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.NghiVangService
{
    public class NghiVangService : INghiVangService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<NghiVang> _uniqueIdGenerator;
        private readonly NghiVangProfile nghiVangProfile;
        private readonly RecalculateSalary recalculateSalary;

        public NghiVangService(HumanResourceManagementDbContext dbContext, IUniqueIdGenerator<NghiVang> uniqueIdGenerator, 
            NghiVangProfile nghiVangProfile, RecalculateSalary recalculateSalary)
        {
            _dbContext = dbContext;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.nghiVangProfile = nghiVangProfile;
            this.recalculateSalary = recalculateSalary;
        }

        public async Task<ICollection<object>> GetNghiVangs()
        {
            ICollection<NghiVang> nghiVangs = await _dbContext.NghiVangs
               .Include(nv => nv.NhanVien)
               .ToListAsync();

            return nghiVangProfile.MapToListDto(nghiVangs);
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10)
        {
            // Lấy tổng số lượng mục
            var totalCount = _dbContext.HopDongs.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

   

            // Sử dụng Skip() và Take() để phân trang
            var items = _dbContext.NghiVangs
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Include(h => h.NhanVien)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = nghiVangProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<ICollection<object>> GetNghiVangByMaNhanVien(string maNhanVien)
        {
            ICollection<NghiVang> nghiVangs = await _dbContext.NghiVangs
               .Include(nv => nv.NhanVien)
               .Where(nv => nv.NhanVien.MaNhanVien == maNhanVien)
               .ToListAsync();

            return nghiVangProfile.MapToListDto(nghiVangs);
        }

        public async Task<object> ThemNghiVang(NghiVangDTO nghiVangDTO)
        {
            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(nghiVangDTO.NhanVienId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            ChamCong? chamCong = await _dbContext.ChamCongs
               .SingleOrDefaultAsync(cc =>
                   nghiVangDTO.NgayBatDau <= cc.NgayChamCong
                   && nghiVangDTO.NgayKetThuc >= cc.NgayChamCong
                   && cc.NhanVienId.Equals(nhanVien.MaNhanVien)
               );

            if (chamCong != null)
                throw new ConflictException($"Nhân viên đã được chấm công vào ngày {chamCong.NgayChamCong}");

            string maNghiVang = await _uniqueIdGenerator.GetUniqueID("NVG", 8, "MaNghiVang");
            NghiVang nghiVang = new NghiVangBuilder()
                .MaNghiVang(maNghiVang)
                .NgayBatDau(nghiVangDTO.NgayBatDau)
                .NgayKetThuc(nghiVangDTO.NgayKetThuc)
                .LoaiNghiVang(nghiVangDTO.LoaiNghiVang)
                .LyDo(nghiVangDTO.LyDo)
                .GhiChu(nghiVangDTO.GhiChu)
                .TrangThai("Đã duyệt")
                .NhanVien(nhanVien)
                .Build();

            TimeSpan soNgayNghi = nghiVangDTO.NgayKetThuc.Subtract(nghiVangDTO.NgayBatDau);
            nghiVang.SoNgayNghi = soNgayNghi.Days + 1;

            await _dbContext.NghiVangs.AddAsync(nghiVang);
            await _dbContext.SaveChangesAsync();
            await RecalculationSalary(nghiVangDTO, nhanVien.MaNhanVien);

            return nghiVangProfile.MapToDto(nghiVang);
        }

        public async Task<bool> CapNhatNghiVang(string id, NghiVangDTO nghiVangDTO)
        {
            NghiVang? nghiVang = await _dbContext.NghiVangs
                .Include(nv => nv.NhanVien)
                .SingleOrDefaultAsync(dt => dt.MaNghiVang.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy nghỉ vắng");

            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(nghiVangDTO.NhanVienId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            ChamCong? chamCong = await _dbContext.ChamCongs
               .SingleOrDefaultAsync(cc =>
                   nghiVangDTO.NgayBatDau <= cc.NgayChamCong
                   && nghiVangDTO.NgayKetThuc >= cc.NgayChamCong
                   && cc.NhanVienId.Equals(nhanVien.MaNhanVien)
               );

            if (chamCong != null)
                throw new ConflictException($"Nhân viên đã được chấm công vào ngày {chamCong.NgayChamCong}");

            nghiVang.LoaiNghiVang = nghiVangDTO.LoaiNghiVang;
            nghiVang.GhiChu = nghiVangDTO.GhiChu;
            nghiVang.NgayBatDau = nghiVangDTO.NgayBatDau;
            nghiVang.NgayKetThuc = nghiVangDTO.NgayKetThuc;
            nghiVang.LyDo = nghiVangDTO.LyDo;
            nghiVang.NhanVien = nhanVien;

            TimeSpan soNgayNghi = nghiVangDTO.NgayKetThuc.Subtract(nghiVangDTO.NgayBatDau);
            nghiVang.SoNgayNghi = soNgayNghi.Days + 1;

            await _dbContext.SaveChangesAsync();
            await RecalculationSalary(nghiVangDTO, nhanVien.MaNhanVien);

            return true;
        }

        public async Task<bool> XoaNghiVang(string id)
        {
            NghiVang? nghiVang = await _dbContext.NghiVangs
                .SingleOrDefaultAsync(dt => dt.MaNghiVang.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy nghỉ vắng");

            _dbContext.Remove(nghiVang);
            await _dbContext.SaveChangesAsync();

            NghiVangDTO nghiVangDTO = new NghiVangDTO
            {
                MaNghiVang = "NV000000",
                LoaiNghiVang = nghiVang.LoaiNghiVang,
                NgayBatDau = nghiVang.NgayBatDau,
                NgayKetThuc = nghiVang.NgayKetThuc,
                SoNgayNghi = nghiVang.SoNgayNghi,
                LyDo = nghiVang.LyDo,
                TrangThai = nghiVang.TrangThai,
                GhiChu = nghiVang.GhiChu,
                NhanVienId = nghiVang.NhanVienId
            };

            await RecalculationSalary(nghiVangDTO, nghiVang.NhanVienId);

            return true;
        }

        public async Task<ICollection<object>> GetNghiVangsFromTo(string maNhanVien, DateTime month)
        {
            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(maNhanVien))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            ICollection<NghiVang> nghiVangs = await _dbContext.NghiVangs
                .Where(nv =>
                    nv.NhanVienId.Equals(maNhanVien)
                    && ((nv.NgayBatDau.Month == month.Month && nv.NgayBatDau.Year == month.Year)
                    || (nv.NgayKetThuc.Month == month.Month && nv.NgayKetThuc.Year == month.Year))
                ).Include(nv => nv.NhanVien).
                ToListAsync();

            return nghiVangProfile.MapToListDto(nghiVangs);
        }

        private async Task RecalculationSalary(NghiVangDTO nghiVangDTO, string maNhanVien)
        {
            List<Luong> luongNhanVien = await _dbContext.Luongs
                .Where(lg =>
                    ((nghiVangDTO.NgayBatDau.Month == lg.ThangTinhLuong && nghiVangDTO.NgayBatDau.Year == lg.NamTinhLuong)
                    || (nghiVangDTO.NgayKetThuc.Month == lg.ThangTinhLuong && nghiVangDTO.NgayKetThuc.Year == lg.NamTinhLuong))
                    && lg.NhanVienId.Equals(maNhanVien)).ToListAsync();

            if (luongNhanVien.Count > 0)
            {
                foreach (var luong in luongNhanVien)
                {
                    int thang = 0;
                    int nam = 0;
                    if (luong.ThangTinhLuong == nghiVangDTO.NgayBatDau.Month)
                    {
                        thang = nghiVangDTO.NgayBatDau.Month;
                        nam = nghiVangDTO.NgayBatDau.Year;
                    }
                    else if (luong.ThangTinhLuong == nghiVangDTO.NgayKetThuc.Month)
                    {
                        thang = nghiVangDTO.NgayKetThuc.Month;
                        nam = nghiVangDTO.NgayKetThuc.Year;
                    }

                    recalculateSalary.ChangeState(new PaidAbsenceState());

                    await recalculateSalary.CalculateSalary(maNhanVien, thang, nam);
                }
            }
            else
            {
                recalculateSalary.ChangeState(new UnpaidAbsenceState());
            }
        }
    }
}
