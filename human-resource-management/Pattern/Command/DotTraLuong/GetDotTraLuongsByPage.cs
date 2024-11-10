using HumanResourceManagement.Response;
using HumanResourceManagement.Services.DotTraLuongService;

namespace HumanResourceManagement.Pattern.Command.DotTraLuong
{
    public class GetDotTraLuongsByPage : ICrudCommand
    {
        private readonly IDotTraLuongService _service;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly DateTime? month;

        public GetDotTraLuongsByPage(IDotTraLuongService service, int page = 1, int pageSize = 10, DateTime? month = null)
        {
            _service = service;
            _page = page;
            _pageSize = pageSize;
            this.month = month;
        }

        public async Task<ApiResponse> Execute()
        {
            var phongBan = _service.GetListByPage(month, _page, _pageSize);
            return new ApiResponse
            {
                Data = phongBan,
                Message = "Success",
                StatusCode = 200
            };
        }
    }
}
