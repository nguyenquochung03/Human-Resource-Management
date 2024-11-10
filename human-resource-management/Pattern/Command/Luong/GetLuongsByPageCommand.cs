using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LuongService;

namespace HumanResourceManagement.Pattern.Command.Luong
{
    public class GetLuongsByPageCommand : ICrudCommand
    {
        private readonly ILuongService _service;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly DateTime? _month;
        private readonly string _maNhanVien;
        public GetLuongsByPageCommand(ILuongService service, DateTime? month, int page = 1, int pageSize = 10, string maNhanVien = "")
        {
            _service = service;
            _page = page;
            _pageSize = pageSize;
            _month = month;
            _maNhanVien = maNhanVien;
        }

        public async Task<ApiResponse> Execute()
        {
            var data = _service.GetListByPage(_month, _page, _pageSize, _maNhanVien);
            return new ApiResponse
            {
                Data = data,
                Message = "Success",
                StatusCode = 200
            };
        }
    }
}
