using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using HumanResourceManagement.Pattern.Observer;
using HumanResourceManagement.Profile;
using HumanResourceManagement.Services.ThueService;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.LuongService
{
    public class LuongService : ILuongService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly LuongProfile luongProfile;



        public LuongService(
            HumanResourceManagementDbContext context,
            LuongProfile luongProfile
        )
        
        {
            _dbContext = context;
            this.luongProfile = luongProfile;
        
        }

        public async Task<ICollection<object>> GetLuongs()
        {
            ICollection<Luong> luongs = await _dbContext.Luongs
                .Include(t => t.NhanVien)
                .ToListAsync();

            return luongProfile.MapToListDto(luongs);
        }
        public dynamic GetListByPage(DateTime? month, int page = 1, int pageSize = 10, string maNhanVien = "")
        {
            // Lấy tổng số lượng mục
            
            var query = _dbContext.Luongs.AsQueryable();

            if(!string.IsNullOrEmpty(maNhanVien))
            {
                query = query.Where(t => t.NhanVienId.Equals(maNhanVien));
            }

            if(month.HasValue)
            {
                query = query.Where(t => t.ThangTinhLuong.Equals(month.Value.Month) && t.NamTinhLuong.Equals(month.Value.Year));
            }
            

            var totalCount = query.Count();
            // Tính số trang và kiểm tra xem có trang hợp lệ không
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            //if (page < 1 || page > totalPages)
            //{
            //    return new { msg = "Invalid page number" };
            //}

            // Sử dụng Skip() và Take() để phân trang
            var items = query
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Include(t => t.NhanVien)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = luongProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<object> GetLuongById(string id)
        {
            Luong? luong = await _dbContext.Luongs
                .Include(t => t.NhanVien)
                .SingleOrDefaultAsync(t => t.MaLuong.Equals(id))
                ?? throw new NotFoundException("Không tìm thấy lương");

            return luongProfile.MapToDto(luong);
        }

        public async Task<ICollection<object>> GetLuongByMaNhanVien(string maNhanVien)
        {
            ICollection<Luong> luongs = await _dbContext.Luongs
               .Include(t => t.NhanVien)
               .Where(t => t.NhanVienId.Equals(maNhanVien)).ToListAsync();

            return luongProfile.MapToListDto(luongs);
        }
    }
}

