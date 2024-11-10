using HumanResourceManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.SearchEmployeeStrategy
{
    public class NameSearchEmployeeStrategy : ISearchEmployeeStrategy
    {
        private readonly HumanResourceManagementDbContext _context;
        public NameSearchEmployeeStrategy(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<NhanVien>> SearchAsync(string keyword)
        {
            List<NhanVien> nhanViens = await _context.NhanViens
                .Include(nv => nv.PhongBan)
                .Include(nv => nv.ChucVu)
                .Include(nv => nv.PhuCapNhanViens)
                .ThenInclude(nv => nv.PhuCap)
                .Where(nv => nv.HoTen.ToLower().Contains(keyword.ToLower()))
                .ToListAsync();
            return nhanViens;
        }
    }
}
