using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NhanVienService;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public class GetNhaViensByPageCommand : ICrudCommand
    {
        private readonly INhanVienService _nhanVienService;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly string queryType;
        private readonly string queryValue;

        public GetNhaViensByPageCommand(INhanVienService service, string queryType, string queryValue, int page = 1, int pageSize = 10)
        {
            _nhanVienService = service;
            _page = page;
            _pageSize = pageSize;
            this.queryValue = queryValue;
            this.queryType = queryType;
        }

        public async Task<ApiResponse> Execute()
        {
            var nhanViens = await _nhanVienService.GetListByPage(_page, _pageSize, queryType, queryValue);
            return new ApiResponse
            {
                Data = nhanViens,
                Message = "Lấy thông tin nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
