using HumanResourceManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.SearchEmployeeStrategy
{
    public class IdSearchEmployeeStrategy : ISearchEmployeeStrategy
    {
        private readonly HumanResourceManagementDbContext _context;
        public IdSearchEmployeeStrategy(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<NhanVien>> SearchAsync(string id)
        {
            var nhanVien = await _context.NhanViens
                .Include(nv => nv.PhongBan)
                .Include(nv => nv.ChucVu)
                .Include(nv => nv.PhuCapNhanViens)
                .ThenInclude(nv => nv.PhuCap)
                .Where(nv => nv.MaNhanVien == id).FirstOrDefaultAsync();
            return nhanVien != null ? new List<NhanVien> { nhanVien } : new List<NhanVien>();
        }
    }
}
