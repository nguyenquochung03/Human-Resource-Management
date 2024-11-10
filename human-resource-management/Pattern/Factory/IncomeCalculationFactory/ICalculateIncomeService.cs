namespace HumanResourceManagement.Pattern.Factory.CalculateTotalIncome
{
    public interface ICalculateIncomeService
    {
        Task<double> CalculateBonus(string employeeId, int month, int year);
        Task<double> CalculateAllowance(string employeeId);
        Task<double> CalculateTotal(string employeeId, int month, int year);
    }
}
