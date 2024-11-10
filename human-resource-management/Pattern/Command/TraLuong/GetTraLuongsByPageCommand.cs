using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TraLuongService;

namespace HumanResourceManagement.Pattern.Command.TraLuong
{
    public class GetTraLuongsByPageCommand : ICrudCommand
    {
        private readonly ITraLuongService _service;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly string _employeeName;
        private readonly DateTime? month;

        public GetTraLuongsByPageCommand(ITraLuongService service, int page = 1, int pageSize = 10, string employeeName = null, DateTime? month = null)
        {
            _service = service;
            _page = page;
            _pageSize = pageSize;
            _employeeName = employeeName;
            this.month = month;
        }

        public async Task<ApiResponse> Execute()
        {
            var data = _service.GetListByPage(_page, _pageSize, _employeeName, month);
            return new ApiResponse
            {
                Data = data,
                Message = "Success",
                StatusCode = 200
            };
        }
    }
}
