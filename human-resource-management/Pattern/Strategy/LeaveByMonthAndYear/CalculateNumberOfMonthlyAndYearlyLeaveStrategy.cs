using HumanResourceManagement.Constants;
using HumanResourceManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Strategy.LeaveByMonthAndYear
{
    public class CalculateNumberOfMonthlyAndYearlyLeaveStrategy : ICalculateNumberOfLeaveByMonthAndYearStrategy
    {
        private readonly HumanResourceManagementDbContext _dbContext;

        public CalculateNumberOfMonthlyAndYearlyLeaveStrategy(HumanResourceManagementDbContext context)
        {
            _dbContext = context;
        }

        public List<NghiVang> GetLeaveByMonthAndYear(string employeeId, int thang, int nam)
        {
            List<NghiVang> leaveByMonthAndYearList = _dbContext.NghiVangs
                    .Where(n => n.NhanVienId == employeeId
                        && ((n.NgayBatDau.Month == thang && n.NgayBatDau.Year == nam) ||
                        (n.NgayKetThuc.Month == thang && n.NgayKetThuc.Year == nam)))
                    .ToList();

            return leaveByMonthAndYearList;
        }

        public int GetTotalDayByMonth(ICollection<NghiVang> nghiVangs, int thang, int nam)
        {
            int totalDay = 0;

            foreach (NghiVang nghiVang in nghiVangs)
            {
                if (nghiVang.NgayBatDau.Month == thang && nghiVang.NgayKetThuc.Month == thang)
                {
                    TimeSpan duration = (nghiVang.NgayKetThuc - nghiVang.NgayBatDau);
                    totalDay += (int)duration.TotalDays + 1;
                }
                else if (nghiVang.NgayBatDau.Month == thang)
                {
                    DateTime endOfMonth = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang));
                    TimeSpan duration = endOfMonth - nghiVang.NgayBatDau;
                    totalDay += (int)duration.TotalDays + 1;
                }
                else
                {
                    DateTime startOfMonth = new DateTime(nam, thang, 1);
                    TimeSpan duration = nghiVang.NgayKetThuc - startOfMonth;
                    totalDay += (int)duration.TotalDays + 1;
                }
            }

            return totalDay;
        }
    }
}
