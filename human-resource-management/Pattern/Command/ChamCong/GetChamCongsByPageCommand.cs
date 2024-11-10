using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChamCongService;

namespace HumanResourceManagement.Pattern.Command.ChamCong
{
    public class GetChamCongsByPageCommand : ICrudCommand
    {
        private readonly IChamCongService _chamCongService;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly DateTime? day;

        public GetChamCongsByPageCommand(IChamCongService chamCongService, int page = 1, int pageSize = 10, DateTime? day = null)
        {
            _chamCongService = chamCongService;
            _page = page;
            _pageSize = pageSize;
            this.day = day;
        }

        public async Task<ApiResponse> Execute()
        {
            var list = await _chamCongService.GetListByPage(_page, _pageSize, day);
            return new ApiResponse
            {
                Data = list,
                Message = "Lấy thông tin chấm công thành công",
                StatusCode = 200
            };
        }
    }
}
