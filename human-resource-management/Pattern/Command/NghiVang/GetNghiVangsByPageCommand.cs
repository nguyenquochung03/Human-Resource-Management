using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HopDongService;
using HumanResourceManagement.Services.NghiVangService;

namespace HumanResourceManagement.Pattern.Command.NghiVang
{
    public class GetNghiVangsByPageCommand: ICrudCommand
    {
        private readonly INghiVangService _service;
        private readonly int _page;
        private readonly int _pageSize;

        public GetNghiVangsByPageCommand(INghiVangService service, int page = 1, int pageSize = 10)
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
