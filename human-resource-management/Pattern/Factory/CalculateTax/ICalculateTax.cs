namespace HumanResourceManagement.Pattern.Factory.CalculateTax
{
    public interface ICalculateTax
    {
        Task<double> BasicSalaryCalculation();
        Task<double> TotalSalaryCalculation();
        Task<double> TotalInsuranceCalculation();
        Task<double> TotalDeductionsCalculation();
        Task<double> TaxableIncomeCalculation();
        Task<double> TaxRateCalculation();
        Task<double> PersionalIncomeTaxCalculation();
        Task<double> SalaryDetuctionsCalculation();
        Task<double> SalaryReceived();
    }
}
