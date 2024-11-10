using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class GetLichLamsByPageCommand : ICrudCommand
    {
        private readonly ILichLamService _service;
        private readonly int _page;
        private readonly int _pageSize;

        public GetLichLamsByPageCommand(ILichLamService service, int page = 1, int pageSize = 10)
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
