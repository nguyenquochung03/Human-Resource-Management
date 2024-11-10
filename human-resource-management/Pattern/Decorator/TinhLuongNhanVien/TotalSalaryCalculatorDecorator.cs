using HumanResourceManagement.Pattern.Strategy.LeaveByMonthAndYear;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Decorator.TinhLuongNhanVien
{
    public class TotalSalaryCalculatorDecorator : SalaryCalculatorDecorator
    {
        private readonly ICalculateNumberOfLeaveByMonthAndYearStrategy leaveByMonthAndYearStrategy;

        public TotalSalaryCalculatorDecorator(ISalaryCalculator salaryCalculator, HumanResourceManagementDbContext dbContext, int thang, int nam)
            : base(salaryCalculator, dbContext, thang, nam)
        {
            leaveByMonthAndYearStrategy = new CalculateNumberOfMonthlyAndYearlyLeaveBePaidStrategy(dbContext);
        }

        public override async Task<double> CalculateSalary(string employeeId)
        {
            double leaveSalary;

            // Lương cơ bản sau khi chấm công của nhân viên
            double attendaceSalary = await _salaryCalculator.CalculateSalary(employeeId);

            // Lấy tổng giờ làm của nhân viên để tính lương theo giờ
            var totalWorkingHours = await _dbContext.ChamCongs
                .Where(c => c.NhanVienId == employeeId && c.NgayChamCong.Month == thang && c.NgayChamCong.Year == nam)
                .SumAsync(c => c.TongGioLam);

            // Lương theo giờ của nhân viên
            double baseSalary = attendaceSalary / totalWorkingHours;

            // Lấy những lần nghỉ có lương
            var paidLeaveByMonthAndYear = leaveByMonthAndYearStrategy.GetLeaveByMonthAndYear(employeeId, thang, nam);

            // Tổng số giờ nghỉ có lương
            int paidLeaveHours = 0;

            if (paidLeaveByMonthAndYear.Count > 0)
            {
                paidLeaveHours = leaveByMonthAndYearStrategy.GetTotalDayByMonth(paidLeaveByMonthAndYear, thang, nam) * 8;
            }
            
            // Tính lương dựa trên số ngày nghỉ có lương của nhân viên
            leaveSalary = baseSalary * paidLeaveHours;

            // Trả về tổng lương tính cả nghỉ có lương và lương chấm công
            return leaveSalary + attendaceSalary;
        }
    }
}
