using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Strategy.LeaveByMonthAndYear
{
    public interface ICalculateNumberOfLeaveByMonthAndYearStrategy
    {
        public List<NghiVang> GetLeaveByMonthAndYear(string employeeId, int thang, int nam);
        public int GetTotalDayByMonth(ICollection<NghiVang> nghiVangs, int thang, int nam);
    }
}
