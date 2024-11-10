using HumanResourceManagement.Constants;
using HumanResourceManagement.Models;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;
using HumanResourceManagement.Services.ThuongService;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Factory.CalculateTotalIncome
{
    public class CalculateIncomeService : ICalculateIncomeService
    {
        private readonly HumanResourceManagementDbContext _context;
        private readonly IPhuCapService phuCapService;
        private readonly ISalaryCalculationService salaryCalculationService;

        public CalculateIncomeService(HumanResourceManagementDbContext context, IPhuCapService phuCapService, ISalaryCalculationService salaryCalculationService)
        {
            _context = context;
            this.phuCapService = phuCapService;
            this.salaryCalculationService = salaryCalculationService;
        }

        public async Task<double> CalculateAllowance(string employeeId)
        {
            ICollection<PhuCap> phuCaps = await phuCapService.GetPhuCapNhanVienByEmployeeId(employeeId);
            ICollection<PhuCap> phuCapDangHieuLuc = phuCaps.Where(pc => pc.TrangThai.Equals(TrangThaiPhuCap.DangHieuLuc)).ToList();

            return phuCapDangHieuLuc.Sum(pc => pc.SoTienPhuCap);
        }

        public async Task<double> CalculateBonus(string employeeId, int month, int year)
        {
            ICollection<Thuong> thuongs = await _context.Thuongs
                .Include(l => l.ThuongNhanViens)
                .ThenInclude(llnv => llnv.NhanVien)
                .Where(l => l.ThuongNhanViens.Any(llnv => llnv.NhanVien.MaNhanVien.Equals(employeeId)))
                .ToListAsync();
            ICollection<Thuong> thuongThuocThangNam = thuongs.Where(tg => tg.NgayKhenThuong.Month == month && tg.NgayKhenThuong.Year == year).ToList();

            return thuongThuocThangNam.Sum(tg => tg.SoTienThuong);
        }

        public async Task<double> CalculateTotal(string employeeId, int month, int year)
        {
            double allowance = await this.CalculateAllowance(employeeId);

            return allowance + await salaryCalculationService.CalculateSalary(employeeId, month, year);
        }
    }
}
