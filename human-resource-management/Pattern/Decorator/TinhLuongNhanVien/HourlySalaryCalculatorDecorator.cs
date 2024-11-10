namespace HumanResourceManagement.Pattern.Decorator.TinhLuongNhanVien
{
    public class HourlySalaryCalculatorDecorator : SalaryCalculatorDecorator
    {
        public HourlySalaryCalculatorDecorator(ISalaryCalculator salaryCalculator, HumanResourceManagementDbContext dbContext, int thang, int nam)
            : base(salaryCalculator, dbContext, thang, nam)
        { }

        public override async Task<double> CalculateSalary(string employeeId)
        {
            // Lương cơ bản nhân viên
            double baseSalary = await _salaryCalculator.CalculateSalary(employeeId);

            // Tính lương theo giờ của nhân viên theo tháng
            int daysInMonth = DateTime.DaysInMonth(nam, thang);

            int divisor;
            switch (daysInMonth)
            {
                case 28:
                    divisor = 24;
                    break;
                case 29:
                    divisor = 25;
                    break;
                case 30:
                    divisor = 26;
                    break;
                case 31:
                    divisor = 27;
                    break;
                default:
                    divisor = 24;
                    break;
            }

            double daySalary = baseSalary / divisor;
            double hourlySalary = daySalary / 8;

            return hourlySalary;
        }
    }
}
