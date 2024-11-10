namespace HumanResourceManagement.Pattern.Decorator.TinhLuongNhanVien
{
    public abstract class SalaryCalculatorDecorator : ISalaryCalculator
    {
        protected readonly ISalaryCalculator _salaryCalculator;
        protected readonly HumanResourceManagementDbContext _dbContext;
        protected readonly int thang;
        protected readonly int nam;

        public SalaryCalculatorDecorator(ISalaryCalculator salaryCalculator, HumanResourceManagementDbContext dbContext, int thang, int nam)
        {
            _salaryCalculator = salaryCalculator;
            _dbContext = dbContext;
            this.thang = thang;
            this.nam = nam;
        }

        public abstract Task<double> CalculateSalary(string employeeId);
    }
}
