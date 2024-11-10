using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Adapter;
using HumanResourceManagement.Profile;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.TrinhDoHocVanService
{
    public class TrinhDoHocVanService : ITrinhDoHocVanService
    {
        private readonly HumanResourceManagementDbContext _dbContext;
        private readonly IUniqueIdGenerator<TrinhDoHocVan> _uniqueIdGenerator;
        private readonly TrinhDoHocVanProfile trinhDoHocVanProfile;

        public TrinhDoHocVanService(HumanResourceManagementDbContext dbContext, IUniqueIdGenerator<TrinhDoHocVan> _uniqueIdGenerator, TrinhDoHocVanProfile trinhDoHocVanProfile)
        {
            _dbContext = dbContext;
            this._uniqueIdGenerator = _uniqueIdGenerator;
            this.trinhDoHocVanProfile = trinhDoHocVanProfile;
        }

        public dynamic GetListByPage(int page, int pageSize)
        {
            var totalCount = _dbContext.TrinhDoHocVans.Count();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = _dbContext.TrinhDoHocVans
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
                Items = trinhDoHocVanProfile.MapToListDto(items)
            };
            return data;
        }

        public async Task<ICollection<object>> GetTrinhDoHocVan()
        {
            var trinhDoHocVans = await _dbContext.TrinhDoHocVans
                .Include(tdhv => tdhv.NhanVien)
                .ToListAsync();
            return trinhDoHocVanProfile.MapToListDto(trinhDoHocVans);
        }

        public async Task<object> GetTrinhDoHocVanByEmployeeId(string id)
        {
            var trinhDoHocVan = await _dbContext.TrinhDoHocVans
                 .Include(t => t.NhanVien)
                 .Where(t => t.NhanVienId.Equals(id)).ToListAsync();


            return trinhDoHocVanProfile.MapToListDto(trinhDoHocVan);
        }

        public async Task<bool> SuaTrinhDoHocVan(string id, TrinhDoHocVan model)
        {
            var existingTrinhDoHocVan = await _dbContext.TrinhDoHocVans
               .Include(t => t.NhanVien)
               .FirstOrDefaultAsync(tdhv => tdhv.MaTrinhDo == id);

            if (existingTrinhDoHocVan == null) throw new NotFoundException("Không tìm thấy trình độ học vấn");

            _dbContext.Entry(existingTrinhDoHocVan).CurrentValues.SetValues(model);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<object> ThemTrinhDoHocVan(TrinhDoHocVan model)
        {
            model.MaTrinhDo = await _uniqueIdGenerator.GetUniqueID("TDHV", 8, "MaTrinhDo");
            await _dbContext.TrinhDoHocVans.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return trinhDoHocVanProfile.MapToDto(model);
        }

        public async Task<bool> XoaTrinhDoHocVan(string id)
        {
            var trinhDoHocVan = await _dbContext.TrinhDoHocVans.FirstOrDefaultAsync(tdhv => tdhv.MaTrinhDo == id);

            if (trinhDoHocVan == null) throw new NotFoundException("Không tìm thấy trình độ học vấn");

            _dbContext.TrinhDoHocVans.Remove(trinhDoHocVan);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
