using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.HopDongService
{
    public class HopDongService : IHopDongService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<HopDong> _uniqueIdGenerator;
        private readonly HopDongProfile hopDongProfile;

        public HopDongService(HumanResourceManagementDbContext dbContext, IUniqueIdGenerator<HopDong> uniqueIdGenerator, HopDongProfile hopDongProfile)
        {
            _dbContext = dbContext;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.hopDongProfile = hopDongProfile;
        }

        public async Task<ICollection<object>> GetHopDongs()
        {
            ICollection<HopDong> hopDongs = await _dbContext.HopDongs
                .Include(h => h.NhanVien)
                .ToListAsync();

            return hopDongProfile.MapToListDto(hopDongs);
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10)
        {
            // Lấy tổng số lượng mục
            var totalCount = _dbContext.HopDongs.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            //if (page < 1 || page > totalPages)
            //{
            //    return new { msg = "Invalid page number" };
            //}

            // Sử dụng Skip() và Take() để phân trang
            var items = _dbContext.HopDongs
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
                Items = hopDongProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<ICollection<object>> GetHopDongByEmployeeId(string employeeId)
        {
            List<HopDong> hopDongs = await _dbContext.HopDongs
                .Include(h => h.NhanVien)
            .Where(nv => nv.NhanVienId == employeeId)
            .ToListAsync();

            return hopDongProfile.MapToListDto(hopDongs);
        }

        public async Task<object> ThemHopDong(HopDongDTO hopDongDTO)
        {
            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(hopDongDTO.NhanVienId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            HopDong hopDong = new HopDong()
            {
                MaHopDong = await _uniqueIdGenerator.GetUniqueID("HD", 8, "MaHopDong"),
                LoaiHopDong = hopDongDTO.LoaiHopDong,
                NgayBatDau = hopDongDTO.NgayBatDau,
                NgayKetThuc = hopDongDTO.NgayKetThuc,
                NgayKy = hopDongDTO.NgayKy,
                GhiChu = hopDongDTO.GhiChu,
                NoiDungHopDong = hopDongDTO.NoiDungHopDong,
                ThoiHanHopDong = hopDongDTO.ThoiHanHopDong,
                TrangThaiHopDong = hopDongDTO.TrangThaiHopDong,
                NhanVien = nhanVien
            };

            await _dbContext.HopDongs.AddAsync(hopDong);
            await _dbContext.SaveChangesAsync();

            return hopDongProfile.MapToDto(hopDong);
        }

        public async Task<bool> CapNhatHopDong(string id, HopDongDTO hopDongDTO)
        {
            HopDong? hopDong = await _dbContext.HopDongs
                .Include(h => h.NhanVien)
                .SingleOrDefaultAsync(dt => dt.MaHopDong.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy hợp đồng");

            NhanVien? nhanVien = await _dbContext.NhanViens
                .SingleOrDefaultAsync(nv => nv.MaNhanVien.Equals(hopDongDTO.NhanVienId))
                    ?? throw new NotFoundException("Không tìm thấy nhân viên");

            hopDong.LoaiHopDong = hopDongDTO.LoaiHopDong;
            hopDong.NgayBatDau = hopDongDTO.NgayBatDau;
            hopDong.NgayKetThuc = hopDongDTO.NgayKetThuc;
            hopDong.NgayKy = hopDongDTO.NgayKy;
            hopDong.GhiChu = hopDongDTO.GhiChu;
            hopDong.NoiDungHopDong = hopDongDTO.NoiDungHopDong;
            hopDong.ThoiHanHopDong = hopDongDTO.ThoiHanHopDong;
            hopDong.TrangThaiHopDong = hopDongDTO.TrangThaiHopDong;
            hopDong.NhanVien = nhanVien;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> XoaHopDong(string id)
        {
            HopDong? hopDong = await _dbContext.HopDongs
                .SingleOrDefaultAsync(dt => dt.MaHopDong.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy hợp đồng");


            _dbContext.HopDongs.Remove(hopDong);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<object> GetContractById(string contractId)
        {
            HopDong? hopDong = await _dbContext.HopDongs
                .Include(hd => hd.NhanVien)
                .SingleOrDefaultAsync(dt => dt.MaHopDong.Equals(contractId))
                    ?? throw new NotFoundException("Không tìm thấy hợp đồng");

            return hopDongProfile.MapToDto(hopDong);
        }
    }
}
