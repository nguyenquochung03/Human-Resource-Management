using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChucVuService;


namespace HumanResourceManagement.Pattern.Command.ChucVu
{
    public class GetChucVusByPageCommand : ICrudCommand
    {
        private readonly IChucVuService _chucVuService;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly string phongBanId;
        private readonly string searchChucVu;

        public GetChucVusByPageCommand(IChucVuService chamCongService, int page = 1, int pageSize = 10, string phongBanId = "", string searchChucVu = "")
        {
            _chucVuService = chamCongService;
            _page = page;
            _pageSize = pageSize;
            this.phongBanId = phongBanId;
            this.searchChucVu = searchChucVu;
        }

        public async Task<ApiResponse> Execute()
        {
            var list = _chucVuService.GetListByPage(_page, _pageSize, phongBanId, searchChucVu);
            return new ApiResponse
            {
                Data = list,
                Message = "Lấy thông tin chức vụ thành công",
                StatusCode = 200
            };
        }
    }
}
