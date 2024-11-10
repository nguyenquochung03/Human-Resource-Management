using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.ChucVuService
{
    public class ChucVuService : IChucVuService
    {
        private readonly HumanResourceManagementDbContext _context;
        private readonly IUniqueIdGenerator<ChucVu> _uniqueIdGenerator;
        private readonly ChucVuProfile chucVuProfile;

        public ChucVuService(HumanResourceManagementDbContext dbContext, IUniqueIdGenerator<ChucVu> uniqueIdGenerator, ChucVuProfile chucVuProfile)
        {
            _context = dbContext;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.chucVuProfile = chucVuProfile;
        }

        public async Task<object> GetChucVuById(string id)
        {
            var chucVu = await _context.ChucVus
                .FindAsync(id);

            if (chucVu == null)
            {
                throw new NotFoundException("Không tìm thấy chức vụ");
            }
            return chucVuProfile.MapToDto(chucVu);
        }

        public async Task<ICollection<object>> GetChucVuByPhongBanId(string id)
        {
            ICollection<ChucVu> chucVus = await _context.ChucVus
                .Include(cv => cv.PhongBan)
                .Where(cv => cv.PhongBanId == id)
                .ToListAsync();
            return chucVuProfile.MapToList(chucVus);
        }

        public async Task<ICollection<object>> GetChucVus()
        {
            ICollection<ChucVu> chucVus = await _context.ChucVus
                .Include(c => c.PhongBan)
                .ToListAsync();
            return chucVuProfile.MapToList(chucVus);
        }
        public dynamic GetListByPage(int page, int pageSize, string phongBan = "", string searchValue = "")
        {
           
            var query = _context.ChucVus.AsQueryable();

            List<ChucVu> chucVus = _context.ChucVus
               .Include(c => c.PhongBan)
               .ToList();

            if (!string.IsNullOrEmpty(phongBan))
            {
                chucVus = chucVus
                    .Where(t =>
                    t.PhongBanId.Equals(phongBan)
                ).ToList();
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                string lowerCaseSearchValue = searchValue.ToLower();
                chucVus = chucVus
                    .Where(k =>
                    k.TenChucVu.ToLower().Contains(lowerCaseSearchValue)
                ).ToList();
            }

            // Sử dụng Skip() và Take() để phân trang
            chucVus = chucVus
                .Skip((page - 1) * pageSize)
                                .Take(pageSize).ToList();

            var totalCount = chucVus.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = chucVuProfile.MapToList(chucVus)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }

        public async Task<bool> SuaChucVu(string id, ChucVuDTO chucVuDto)
        {
            var existingChucVu = await _context.ChucVus
                .FirstOrDefaultAsync(cv => cv.MaChucVu == id);
            if (existingChucVu == null)
            {
                throw new NotFoundException("Không tìm thấy chức vụ");
            }

            existingChucVu.TenChucVu = chucVuDto.TenChucVu;
            existingChucVu.MucLuong = chucVuDto.MucLuong;
            existingChucVu.MoTaCongViec = chucVuDto.MoTaCongViec;
            existingChucVu.PhongBanId = chucVuDto.PhongBanId;

            await _context.SaveChangesAsync();
           
            return true;
        }

        public async Task<object> ThemChucVu(ChucVuDTO chucVuDto)
        {
            ChucVu chucVu = new ChucVu
            {
                MaChucVu = await _uniqueIdGenerator.GetUniqueID("CV", 8, "MaChucVu"),
                TenChucVu = chucVuDto.TenChucVu,
                MucLuong = chucVuDto.MucLuong,
                MoTaCongViec = chucVuDto.MoTaCongViec
            };

            if (!string.IsNullOrEmpty(chucVuDto.PhongBanId))
            {
                chucVu.PhongBanId = chucVuDto.PhongBanId;
            }

            await _context.ChucVus.AddAsync(chucVu);
            await _context.SaveChangesAsync();

            return chucVu;
        }

        public async Task<bool> XoaChucVu(string id)
        {
            var chucVu = await _context.ChucVus
                .Include(cv => cv.NhanViens)
                .FirstOrDefaultAsync(cv => cv.MaChucVu == id);
            if (chucVu == null)
            {
                throw new NotFoundException("Không tìm thấy chức vụ");
            }

            if(chucVu.NhanViens.Count > 0)
                throw new ConflictException("Không thể xóa chức vụ");

            _context.ChucVus.Remove(chucVu);

            int rowCount = await _context.SaveChangesAsync();
            if (rowCount == 0)
                throw new ConflictException("Xóa thông chức vụ thất bại");

            return true;
        }
    }
}
