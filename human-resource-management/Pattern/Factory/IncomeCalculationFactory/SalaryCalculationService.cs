using HumanResourceManagement.Pattern.Decorator.TinhLuongNhanVien;

namespace HumanResourceManagement.Pattern.Factory.CalculateTotalIncome
{
    public class SalaryCalculationService : ISalaryCalculationService
    {
        private readonly HumanResourceManagementDbContext _dbContext;

        public SalaryCalculationService(HumanResourceManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<double> CalculateSalary(string employeeId, int month, int year)
        {
            ISalaryCalculator baseSalaryCalculator = new BaseSalaryCalculator(_dbContext);

            // Áp dụng decorator vào đối tượng tính lương ban đầu
            SalaryCalculatorDecorator hourlySalaryCalculatorDecorator = new HourlySalaryCalculatorDecorator(baseSalaryCalculator, _dbContext, month, year);
            AttendanceSalaryCalculatorDecorator attendanceSalaryCalculatorDecorator = new AttendanceSalaryCalculatorDecorator(hourlySalaryCalculatorDecorator, _dbContext, month, year);
            TotalSalaryCalculatorDecorator totalSalaryCalculatorDecorator = new TotalSalaryCalculatorDecorator(attendanceSalaryCalculatorDecorator, _dbContext, month, year);

            return await totalSalaryCalculatorDecorator.CalculateSalary(employeeId);
        }
    }
}
