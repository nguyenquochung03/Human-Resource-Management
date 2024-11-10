using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class GetThuongsByPageCommand : ICrudCommand
    {
        private readonly IThuongService _service;
        private readonly int _page;
        private readonly int _pageSize;

        public GetThuongsByPageCommand(IThuongService service, int page = 1, int pageSize = 10)
        {
            _service = service;
            _page = page;
            _pageSize = pageSize;
        }

        public async Task<ApiResponse> Execute()
        {
            var data = _service.GetListByPage(_page, _pageSize);
            return new ApiResponse
            {
                Data = data,
                Message = "Success",
                StatusCode = 200
            };
        }
    }
}
