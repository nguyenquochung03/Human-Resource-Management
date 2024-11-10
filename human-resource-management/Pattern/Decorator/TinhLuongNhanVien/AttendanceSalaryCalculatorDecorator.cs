
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Decorator.TinhLuongNhanVien
{
    public class AttendanceSalaryCalculatorDecorator : SalaryCalculatorDecorator
    {
        public AttendanceSalaryCalculatorDecorator(ISalaryCalculator salaryCalculator, HumanResourceManagementDbContext dbContext, int thang, int nam)
            : base(salaryCalculator, dbContext, thang, nam)
        {}

        public override async Task<double> CalculateSalary(string employeeId)
        {
            // Lương theo giờ của nhân viên
            var baseSalary = await _salaryCalculator.CalculateSalary(employeeId);

            var totalWorkingHours = await _dbContext.ChamCongs
                .Where(c => c.NhanVienId == employeeId && c.NgayChamCong.Month == thang && c.NgayChamCong.Year == nam)
                .SumAsync(c => c.TongGioLam);

            // Trả về lương theo số giờ làm của nhân viên
            return baseSalary * totalWorkingHours;
        }
    }
}
