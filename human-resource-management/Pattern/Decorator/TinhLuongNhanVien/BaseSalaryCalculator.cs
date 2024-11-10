using HumanResourceManagement.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Pattern.Decorator.TinhLuongNhanVien
{
    public class BaseSalaryCalculator : ISalaryCalculator
    {
        private readonly HumanResourceManagementDbContext _dbContext;

        public BaseSalaryCalculator(HumanResourceManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<double> CalculateSalary(string employeeId)
        {
            var employee = await _dbContext.NhanViens.FirstOrDefaultAsync(e => e.MaNhanVien == employeeId);
            if (employee == null)
            {
                throw new NotFoundException("Không tìm thấy nhân viên");
            }

            return employee.MucLuong;
        }
    }
}
