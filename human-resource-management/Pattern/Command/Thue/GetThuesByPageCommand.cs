using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThueService;

namespace HumanResourceManagement.Pattern.Command.Thue
{
    public class GetThuesByPageCommand : ICrudCommand
    {
        private readonly IThueService _service;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly string employeeId;
        private readonly DateTime? _filterTime;

        public GetThuesByPageCommand(IThueService service, int page = 1, int pageSize = 10, string employeeId = "", DateTime? filterTime = null)
        {
            _service = service;
            _page = page;
            _pageSize = pageSize;
            this.employeeId = employeeId;
            _filterTime = filterTime;
        }

        public async Task<ApiResponse> Execute()
        {
            var data = _service.GetListByPage(_page, _pageSize, employeeId, _filterTime);
            return new ApiResponse
            {
                Data = data,
                Message = "Success",
                StatusCode = 200
            };
        }
    }
}
