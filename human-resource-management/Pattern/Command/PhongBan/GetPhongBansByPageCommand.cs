using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhongBanService;

namespace HumanResourceManagement.Pattern.Command.PhongBan
{
    public class GetPhongBansByPageCommand : ICrudCommand
    {
        private readonly IPhongBanService _phongBanService;
        private readonly int _page;
        private readonly int _pageSize;

        public GetPhongBansByPageCommand(IPhongBanService phongBanService, int page = 1, int pageSize = 10)
        {
            _phongBanService = phongBanService;
            _page = page;
            _pageSize = pageSize;
        }

        public async Task<ApiResponse> Execute()
        {
            var phongBan = _phongBanService.GetListByPage(_page, _pageSize);
            return new ApiResponse
            {
                Data = phongBan,
                Message = "Lấy thông tin phòng ban thành công",
                StatusCode = 200
            };
        }

    }
}
