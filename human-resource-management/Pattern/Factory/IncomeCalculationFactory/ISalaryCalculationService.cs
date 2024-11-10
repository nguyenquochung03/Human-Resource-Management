namespace HumanResourceManagement.Pattern.Factory.CalculateTotalIncome
{
    public interface ISalaryCalculationService
    {
        Task<double> CalculateSalary(string employeeId, int month, int year);
    }
}
