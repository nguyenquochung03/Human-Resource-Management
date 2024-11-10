using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.SearchEmployeeStrategy;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Strategy.SearchEmployeeStrategy
{
    public class SearchAllEmployeeStrategy : ISearchEmployeeStrategy
    {
        private readonly HumanResourceManagementDbContext _context;
        public SearchAllEmployeeStrategy(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<NhanVien>> SearchAsync(string keyword)
        {
            List<NhanVien> nhanViens = await _context.NhanViens
                .Include(nv => nv.ChucVu)
                .Include(nv => nv.LichLamNhanViens)
                .ThenInclude(nv => nv.LichLam)
                .Include(nv => nv.PhuCapNhanViens)
                .ThenInclude(nv => nv.PhuCap)
                .Include(nv => nv.PhongBan)
                .ToListAsync();

            int count = nhanViens.Count;
            Console.WriteLine(count);
            return nhanViens;
        }
    }
}
