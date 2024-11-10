using HumanResourceManagement.DTOs;


namespace HumanResourceManagement.Pattern.Factory.CalculateTax
{
    public class CalculateTaxFactory
    {
        private readonly HumanResourceManagementDbContext _context;
        private readonly ThueDTO _thueDTO;
        private readonly IIncomeCalculationFactory incomeCalculationFactory;

        public CalculateTaxFactory(HumanResourceManagementDbContext context, ThueDTO thueDTO, IIncomeCalculationFactory incomeCalculationFactory)
        {
            _context = context;
            _thueDTO = thueDTO;
            this.incomeCalculationFactory = incomeCalculationFactory;
        }

        public ICalculateTax CreateCalculateTax()
        {
            return new CalculateTax(_context, _thueDTO, incomeCalculationFactory);
        }
    }
}
