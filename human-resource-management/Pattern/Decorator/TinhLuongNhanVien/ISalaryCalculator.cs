namespace HumanResourceManagement.Pattern.Decorator.TinhLuongNhanVien
{
    public interface ISalaryCalculator
    {
        Task<double> CalculateSalary(string employeeId);
    }
}
