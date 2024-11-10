using HumanResourceManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.SearchEmployeeStrategy
{
    public class PhoneSearchEmployeeStrategy : ISearchEmployeeStrategy
    {
        private readonly HumanResourceManagementDbContext _context;
        public PhoneSearchEmployeeStrategy(HumanResourceManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<NhanVien>> SearchAsync(string phoneNumber)
        {
            List<NhanVien> nhanVien = await _context.NhanViens
                .Include(nv => nv.PhongBan)
                .Include(nv => nv.ChucVu)
                .Include(nv => nv.PhuCapNhanViens)
                .ThenInclude(nv => nv.PhuCap)
                .Where(nv => nv.SoDienThoai.Contains(phoneNumber))
                .ToListAsync();
            return nhanVien;
        }
    }
}
