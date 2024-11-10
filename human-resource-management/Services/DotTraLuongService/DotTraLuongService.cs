using HumanResourceManagement.Constants;
using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HumanResourceManagement.Services.DotTraLuongService
{
    public class DotTraLuongService : IDotTraLuongService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<DotTraLuong> _uniqueIdGenerator;
        private readonly DotTraLuongProfile dotTraLuongProfile;

        public DotTraLuongService(HumanResourceManagementDbContext dbContext, IUniqueIdGenerator<DotTraLuong> uniqueIdGenerator, DotTraLuongProfile dotTraLuongProfile)
        {
            _dbContext = dbContext;
            _uniqueIdGenerator = uniqueIdGenerator;
            this.dotTraLuongProfile = dotTraLuongProfile;
        }

        public async Task<ICollection<object>> GetDotTraLuongs()
        {
            ICollection<DotTraLuong> dotTraLuongs = await _dbContext.DotTraLuongs.ToListAsync();

            return dotTraLuongProfile.MapToListDto(dotTraLuongs);
        }
        public dynamic GetListByPage(DateTime? month = null, int page = 1, int pageSize = 10)
        {
            // Lấy tổng số lượng mục
            
            var query = _dbContext.DotTraLuongs.AsQueryable();

            if(month.HasValue)
            {
                query = query.Where(dtl 
                    => dtl.ThangDotTraLuong.Equals(month.Value.Month)
                    && dtl.NamDotTraLuong.Equals(month.Value.Year)
                );
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
                                .ToList();
            var data = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = dotTraLuongProfile.MapToListDto(items)
            };
            // Trả về dữ liệu và thông tin phân trang
            return data;
        }
        public async Task<object> GetDotTraLuongById(string id)
        {
            DotTraLuong? dotTraLuong = await _dbContext.DotTraLuongs
               .SingleOrDefaultAsync(dt => dt.MaDotTraLuong.Equals(id))
                   ?? throw new NotFoundException("Không tìm thấy đợt trả lương");

            return dotTraLuongProfile.MapToDto(dotTraLuong);
        }

        public async Task<bool> XoaDotTraLuong(string id)
        {
            DotTraLuong? dotTraLuong = await _dbContext.DotTraLuongs
                .Include(t => t.TraLuongs)
                .SingleOrDefaultAsync(dt => dt.MaDotTraLuong.Equals(id))
                    ?? throw new NotFoundException("Không tìm thấy đợt trả lương");

            if (dotTraLuong.TraLuongs.Count() > 0)
                throw new ConflictException("Không thể xóa đợt trả lương hiện tại");

            _dbContext.DotTraLuongs.Remove(dotTraLuong);
            await _dbContext.SaveChangesAsync();

            return true;
        }


    }
}
