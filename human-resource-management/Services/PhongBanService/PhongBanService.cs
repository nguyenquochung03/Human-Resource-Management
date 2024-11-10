using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HumanResourceManagement.Services.PhongBanService
{
    public class PhongBanService : IPhongBanService
    {
        private readonly HumanResourceManagementDbContext _context;
        private readonly IUniqueIdGenerator<PhongBan> _uniqueIdGenerator;

        public PhongBanService(HumanResourceManagementDbContext context, IUniqueIdGenerator<PhongBan> uniqueIdGenerator)
        {
            _context = context;
            _uniqueIdGenerator = uniqueIdGenerator;
        }

        public async Task<PhongBan> GetPhongBanById(string id)
        {
            var phongBan = await _context.PhongBans.FindAsync(id);

            if (phongBan == null)
            {
                throw new NotFoundException("Không tìm thấy phòng ban");
            };

            return phongBan;
        }

        public async Task<ICollection<PhongBan>> GetPhongBans()
        {
            ICollection<PhongBan> phongBans = await _context.PhongBans.ToListAsync();
            return phongBans;
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10)
        {
            // Lấy tổng số lượng mục
            var totalCount = _context.PhongBans.Count();

            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Sử dụng Skip() và Take() để phân trang
            var items = _context.PhongBans
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = items
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }

        public async Task<PhongBan> ThemPhongBan(PhongBanDTO phongBanDto)
        {
            PhongBan phongBan = new PhongBan
            {
                SoPhong = await _uniqueIdGenerator.GetUniqueID("PB", 8, "SoPhong"),
                TenPhong = phongBanDto.TenPhong,
                Email = phongBanDto.Email,
                DiaDiem = phongBanDto.DiaDiem,
                MoTa = phongBanDto.MoTa
            };

            if (!phongBanDto.NguoiQuanLy.IsNullOrEmpty())
            {
                phongBan.NguoiQuanLy = phongBanDto.NguoiQuanLy;
            }
            else
            {
                phongBan.NguoiQuanLy = "";
            }

            await _context.PhongBans.AddAsync(phongBan);
            await _context.SaveChangesAsync();

            return phongBan;
        }

        public async Task<bool> SuaPhongBan(string id, PhongBanDTO phongBanDto)
        {
            var existingPhongBan = await _context.PhongBans.FirstOrDefaultAsync(pb => pb.SoPhong == id);
            if (existingPhongBan == null)
            {
                throw new NotFoundException("Không tìm thấy phòng ban");
            }

            existingPhongBan.TenPhong = phongBanDto.TenPhong;
            existingPhongBan.NguoiQuanLy = phongBanDto.NguoiQuanLy;
            existingPhongBan.Email = phongBanDto.Email;
            existingPhongBan.DiaDiem = phongBanDto.DiaDiem;
            existingPhongBan.MoTa = phongBanDto.MoTa;

           await _context.SaveChangesAsync();
        
            return true;
        }

        public async Task<bool> XoaPhongBan(string id)
        {
            var phongBan = await _context.PhongBans
                .Include(pb => pb.ChucVus)
                .Include(pb => pb.NhanViens)
                .FirstOrDefaultAsync(pb => pb.SoPhong == id);
            if (phongBan == null)
            {
                throw new NotFoundException("Không tìm thấy phòng ban");
            }

            if(phongBan.ChucVus.Count > 0 || phongBan.NhanViens.Count > 0)
                throw new ConflictException("Không thể xóa phòng ban");
            _context.PhongBans.Remove(phongBan);

            int rowCount = await _context.SaveChangesAsync();
            if (rowCount == 0)
                throw new ConflictException("Xóa phòng ban không thành công");

            return true;
        }
    }
}
