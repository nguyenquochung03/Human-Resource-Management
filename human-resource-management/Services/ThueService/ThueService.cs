using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.ThueService
{
    public class ThueService : IThueService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly ThueProfile thueProfile;

        public ThueService(HumanResourceManagementDbContext context, ThueProfile thueProfile)
        {
            _dbContext = context;
            this.thueProfile = thueProfile;
        }

        public async Task<ICollection<object>> GetThues()
        {
            ICollection<Thue> thues = await _dbContext.Thues
                .Include(t => t.NhanVien)
                .ToListAsync();

            return thueProfile.MapToListDto(thues);
        }
        public dynamic GetListByPage(int page = 1, int pageSize = 10, string employeeId = "", DateTime? filterMonth = null)
        {
            var query = _dbContext.Thues.AsQueryable();
            if (!string.IsNullOrEmpty(employeeId))
            {
                query = query.Include(t => t.NhanVien).Where(t => 
                    t.NhanVien.MaNhanVien.Equals(employeeId)
                );
            }

            if (filterMonth.HasValue)
            {
                int month = filterMonth.Value.Month;
                int year = filterMonth.Value.Year;

                query = query.Where(t => t.NamTinhThue == year && t.ThangTinhThue == month);
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
                                .Include(h => h.NhanVien)
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = thueProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<object> GetThueById(string id)
        {
            Thue? thue = await _dbContext.Thues
                .Include(t => t.NhanVien)
                .SingleOrDefaultAsync(t => t.MaSoThue.Equals(id))
                ?? throw new NotFoundException("Không tìm thấy thông tin thuế");

            return thueProfile.MapToDto(thue);
        }

        public async Task<ICollection<object>> GetThueByMaNhanVien(string maNhanVien)
        {
            ICollection<Thue> thue = await _dbContext.Thues
                .Include(t => t.NhanVien)
                .Where(t => t.NhanVienId.Equals(maNhanVien)).ToListAsync();

            return thueProfile.MapToListDto(thue);
        }

        public async Task<object> GetThueByNhanVienVaThoiGian(string maNhanVien, int thang, int nam)
        {
            Thue? thue = await _dbContext.Thues
                .Include(t => t.NhanVien)
                .SingleOrDefaultAsync(t
                    => t.NhanVienId.Equals(maNhanVien)
                    && t.ThangTinhThue == thang
                    && t.NamTinhThue == nam
                 )
                ?? throw new NotFoundException("Không tìm thấy thông tin thuế");

            return thueProfile.MapToDto(thue);
        }
    }
}
