using HumanResourceManagement.Pattern.SearchEmployeeStrategy;
using HumanResourceManagement.Pattern.Strategy.SearchEmployeeStrategy;

namespace HumanResourceManagement.Pattern.Factory.SearchEmployeeStrategyFactory
{
    public class SearchEmployeeStrategyFactory
    {
        private readonly HumanResourceManagementDbContext _dbContext;

        public SearchEmployeeStrategyFactory(HumanResourceManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ISearchEmployeeStrategy CreateStrategy(string strategyType)
        {
            switch (strategyType)
            {
                case "id":
                    return new IdSearchEmployeeStrategy(_dbContext);
                case "name":
                    return new NameSearchEmployeeStrategy(_dbContext);
                case "phone":
                    return new PhoneSearchEmployeeStrategy(_dbContext);
                default:
                    return new SearchAllEmployeeStrategy(_dbContext);
            }
        }
    }
}