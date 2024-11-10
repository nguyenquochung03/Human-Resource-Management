using HumanResourceManagement.Pattern.Factory.CalculateTotalIncome;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Factory.IncomeCalculationFactory
{
    public class ConcreteCalculateTotalIncomeFactory : IIncomeCalculationFactory
    {
        private readonly HumanResourceManagementDbContext _context;
        private readonly IPhuCapService _phuCapService;

        public ConcreteCalculateTotalIncomeFactory(HumanResourceManagementDbContext context, IPhuCapService phuCapService)
        {
            _context = context;
            _phuCapService = phuCapService;
        }

        public ISalaryCalculationService CreateSalaryCalculationService()
        {
            return new SalaryCalculationService(_context);
        }

        public ICalculateIncomeService CreateCalculateIncomeService()
        {
            return new CalculateIncomeService(_context, _phuCapService, CreateSalaryCalculationService());
        }
    }
}
