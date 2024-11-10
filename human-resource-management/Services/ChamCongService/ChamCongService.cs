using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Adapter.Create_ChamcongObserver;
using HumanResourceManagement.Pattern.Factory.CalculateTax;
using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using HumanResourceManagement.Pattern.Observer.ChamCongObserver;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Globalization;

namespace HumanResourceManagement.Services.ChamCongService
{
    public class ChamCongService : IChamCongService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<ChamCong> _uniqueIdGenerator;
        private readonly ChamCongProfile chamCongProfile;
        private readonly ICreateChamCongObserverTarget createChamCongObserverTarget;

        public ChamCongService(
            HumanResourceManagementDbContext dbContext,
            IUniqueIdGenerator<ChamCong> uniqueIdGenerator,
            ChamCongProfile chamCongProfile,
            ICreateChamCongObserverTarget createChamCongObserverTarget
        )
        {
            _dbContext = dbContext;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.chamCongProfile = chamCongProfile;
            this.createChamCongObserverTarget = createChamCongObserverTarget;
        }

        public async Task<ICollection<object>> GetChamCongs()
        {
            ICollection<ChamCong> chamCongs = await _dbContext.ChamCongs
                .Include(c => c.NhanVien)
                .ToListAsync();

            return chamCongProfile.MapToListDto(chamCongs);
        }

        public async Task<dynamic> GetListByPage(int page, int pageSize, DateTime? day = null)
        {

            var query = _dbContext.ChamCongs.AsQueryable();

            if(day.HasValue )
            {
                query = query.Where(chc => chc.NgayChamCong.Date == day.Value);
            }
            // Lấy tổng số lượng mục
            var totalCount = query.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            // Sử dụng Skip() và Take() để phân trang
            var items = await query
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Include(c => c.NhanVien)
                                .ToListAsync();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = chamCongProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }

        public async Task<ICollection<object>> GetChamCongByMaNhanVien(string maNhanVien)
        {
            ICollection<ChamCong> chamCongs = await _dbContext.ChamCongs
                .Include(c => c.NhanVien)
               .Where(c => c.NhanVienId.Equals(maNhanVien))
               .ToListAsync();

            return chamCongProfile.MapToListDto(chamCongs);
        }

        public async Task<object> ThemChamCong(ChamCongDTO chamCongDTO)
        {
            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(chamCongDTO.NhanVienId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            NghiVang? nghiVang = await _dbContext.NghiVangs
                .SingleOrDefaultAsync(nv =>
                    nv.NgayBatDau <= chamCongDTO.NgayChamCong
                    && nv.NgayKetThuc >= chamCongDTO.NgayChamCong
                    && nv.NhanVienId.Equals(chamCongDTO.NhanVienId)
                );

            if (nghiVang != null)
                throw new ConflictException($"Nhân viên đã xin nghỉ vắng {nghiVang.LoaiNghiVang} vào ngày {chamCongDTO.NgayChamCong}");

            bool isExisted = await _dbContext.ChamCongs.AnyAsync(chc =>
                chc.NgayChamCong.Day == chamCongDTO.NgayChamCong.Day &&
                chc.NgayChamCong.Month == chamCongDTO.NgayChamCong.Month &&
                chc.NgayChamCong.Year == chamCongDTO.NgayChamCong.Year &&
                chc.NhanVienId.Equals(chamCongDTO.NhanVienId)
            );

            if (isExisted) throw new ConflictException($"Ngày bạn chọn, nhân viên {nhanVien.HoTen} đã được chấm công");

            ChamCong chamCong = new ChamCong()
            {
                MaChamCong = await _uniqueIdGenerator.GetUniqueID("CC", 8, "MaChamCong"),
                NgayChamCong = chamCongDTO.NgayChamCong,
                SoGioLamThem = chamCongDTO.SoGioLamThem,
                SoGioNghiKhongPhep = chamCongDTO.SoGioNghiKhongPhep,
                SoGioNghiPhep = chamCongDTO.SoGioNghiPhep,
                NhanVien = nhanVien,
            };

            (string minGioBatDau, string maxGioKetThuc) = GetMinAndMaxTime(nhanVien.MaNhanVien, chamCongDTO.NgayChamCong);
            if (minGioBatDau != null && maxGioKetThuc != null)
            {
                chamCong.GioVaoLam = minGioBatDau;
                chamCong.GioRaLam = maxGioKetThuc;
            }
            else
            {
                chamCong.GioVaoLam = "00:00";
                chamCong.GioRaLam = "00:00";
            }

            double tongSoGioLam = CalculateTotalWorkingHours(nhanVien.MaNhanVien, chamCongDTO.NgayChamCong);
            chamCong.SoGioLamViec = tongSoGioLam;

            chamCong.TongGioLam = (tongSoGioLam + chamCongDTO.SoGioLamThem) - (chamCongDTO.SoGioNghiKhongPhep + chamCongDTO.SoGioNghiPhep); ;

            await _dbContext.ChamCongs.AddAsync(chamCong);
            await _dbContext.SaveChangesAsync();

            await createChamCongObserverTarget.CreateChamCongObserver(nhanVien.MaNhanVien, chamCongDTO.NgayChamCong.Month, chamCongDTO.NgayChamCong.Year);

            return chamCongProfile.MapToDto(chamCong);
        }

        public async Task<bool> SuaChamCong(string id, ChamCongDTO chamCongDTO)
        {
            ChamCong? chamCong = await _dbContext.ChamCongs
                .SingleOrDefaultAsync(dt => dt.MaChamCong.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy chấm công");

            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(chamCongDTO.NhanVienId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            NghiVang? nghiVang = await _dbContext.NghiVangs
               .SingleOrDefaultAsync(nv =>
                   nv.NgayBatDau <= chamCongDTO.NgayChamCong
                   && nv.NgayKetThuc >= chamCongDTO.NgayChamCong
                   && nv.NhanVienId.Equals(chamCongDTO.NhanVienId)
               );

            if (nghiVang != null)
                throw new ConflictException($"Nhân viên đã xin nghỉ vắng {nghiVang.LoaiNghiVang} vào ngày {chamCongDTO.NgayChamCong}");

            chamCong.NgayChamCong = chamCongDTO.NgayChamCong;
            chamCong.SoGioLamThem = chamCongDTO.SoGioLamThem;
            chamCong.SoGioNghiKhongPhep = chamCongDTO.SoGioNghiKhongPhep;
            chamCong.SoGioNghiPhep = chamCongDTO.SoGioNghiPhep;
            chamCong.NhanVien = nhanVien;

            (string minGioBatDau, string maxGioKetThuc) = GetMinAndMaxTime(nhanVien.MaNhanVien, chamCongDTO.NgayChamCong);
            if (minGioBatDau != null && maxGioKetThuc != null)
            {
                chamCong.GioVaoLam = minGioBatDau;
                chamCong.GioRaLam = maxGioKetThuc;
            }
            else
            {
                chamCong.GioVaoLam = "00:00";
                chamCong.GioRaLam = "00:00";
            }

            double tongSoGioLam = CalculateTotalWorkingHours(nhanVien.MaNhanVien, chamCongDTO.NgayChamCong);
            chamCong.SoGioLamViec = tongSoGioLam;

            chamCong.TongGioLam = (tongSoGioLam + chamCongDTO.SoGioLamThem) - (chamCongDTO.SoGioNghiKhongPhep + chamCongDTO.SoGioNghiPhep); ;

            await _dbContext.SaveChangesAsync();

            await createChamCongObserverTarget.CreateChamCongObserver(nhanVien.MaNhanVien, chamCongDTO.NgayChamCong.Month, chamCongDTO.NgayChamCong.Year);

            return true;

        }

        public async Task<bool> XoaChamCong(string id)
        {
            ChamCong? chamCong = await _dbContext.ChamCongs
               .SingleOrDefaultAsync(dt => dt.MaChamCong.Equals(id))
                   ?? throw new NotFoundException("Không tìm thấy chấm công");

            _dbContext.ChamCongs.Remove(chamCong);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<object>> GetChamCongsByDay(DateTime day)
        {
            ICollection<ChamCong> chamCongs = await _dbContext.ChamCongs
                .Include(chc => chc.NhanVien)
                .Where(chc =>
                    chc.NgayChamCong.Day.Equals(day.Day)
                    && chc.NgayChamCong.Month.Equals(day.Month)
                    && chc.NgayChamCong.Year.Equals(day.Year)
                ).ToListAsync();

            return chamCongProfile.MapToListDto(chamCongs);
        }

        public async Task<ICollection<object>> GetChamCongsFromTo(DateTime from, DateTime to, string employeeId)
        {
            ICollection<ChamCong> chamCongs = await _dbContext.ChamCongs
                .Include(chc => chc.NhanVien)
                .Where(chc =>
                   chc.NhanVienId.Equals(employeeId) && (chc.NgayChamCong >= from && chc.NgayChamCong <= to)
                ).ToListAsync();

            return chamCongProfile.MapToListDto(chamCongs);
        }

        private double CalculateTotalWorkingHours(string employeeId, DateTime workday)
        {
            var lichLam = _dbContext.LichLams
            .Where(l => l.LichLamNhanViens.Any(llnv =>
                llnv.NhanVien.MaNhanVien == employeeId &&
                llnv.LichLam.NgayLam.Date == workday.Date));

            if (lichLam.Any())
            {

                double totalWorkingHours = 0;
                string[] formats = { "h\\:mm", "hh\\:mm" };

                foreach (var lich in lichLam)
                {
                    TimeSpan startTime = TimeSpan.ParseExact(lich.GioBatDau, formats, CultureInfo.InvariantCulture);
                    TimeSpan endTime = TimeSpan.ParseExact(lich.GioKetThuc, formats, CultureInfo.InvariantCulture);
                    TimeSpan workingDuration = endTime.Subtract(startTime);
                    totalWorkingHours += workingDuration.TotalHours;
                }

                return totalWorkingHours;
            }

            throw new NotFoundException($"Ngày {workday.ToString("dd/MM/yyyy")} không có lịch làm của nhân viên");
        }

        private (string minGioBatDau, string maxGioKetThuc) GetMinAndMaxTime(string nhanVienId, DateTime ngayLam)
        {
            string minGioBatDau = null;
            string maxGioKetThuc = null;

            var lichLamNhanVien = _dbContext.LichLamNhanViens
                .Include(llnv => llnv.LichLam)
                .Where(llnv => llnv.NhanVienId == nhanVienId && llnv.LichLam.NgayLam.Date == ngayLam.Date)
                .ToList();

            if (lichLamNhanVien.Count > 0)
            {
                string[] formats = { "h\\:mm", "hh\\:mm" };

                var gioBatDauList = lichLamNhanVien.Select(llnv => TimeSpan.ParseExact(llnv.LichLam.GioBatDau, formats, CultureInfo.InvariantCulture)).ToList();
                var gioKetThucList = lichLamNhanVien.Select(llnv => TimeSpan.ParseExact(llnv.LichLam.GioKetThuc, formats, CultureInfo.InvariantCulture)).ToList();

                var minGioBatDauTimeSpan = gioBatDauList.Min();
                var maxGioKetThucTimeSpan = gioKetThucList.Max();

                minGioBatDau = minGioBatDauTimeSpan.ToString("hh\\:mm");
                maxGioKetThuc = maxGioKetThucTimeSpan.ToString("hh\\:mm");
            }

            return (minGioBatDau, maxGioKetThuc);
        }
    }
}
