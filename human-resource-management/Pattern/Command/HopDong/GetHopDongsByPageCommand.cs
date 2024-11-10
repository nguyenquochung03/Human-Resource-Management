using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HopDongService;

namespace HumanResourceManagement.Pattern.Command.HopDong
{
    public class GetHopDongsByPageCommand : ICrudCommand
    {
        private readonly IHopDongService _service;
        private readonly int _page;
        private readonly int _pageSize;

        public GetHopDongsByPageCommand(IHopDongService service, int page = 1, int pageSize = 10)
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
