using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;

namespace HumanResourceManagement.Pattern.Factory
{
    public interface IIncomeCalculationFactory
    {
        ISalaryCalculationService CreateSalaryCalculationService();
        ICalculateIncomeService CreateCalculateIncomeService();
    }
}