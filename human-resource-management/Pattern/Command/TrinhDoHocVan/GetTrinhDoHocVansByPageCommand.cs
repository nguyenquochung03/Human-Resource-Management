using HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TrinhDoHocVanService;

namespace HumanResourceManagement.Pattern.Command.TrinhDoHocVan
{
    public class GetTrinhDoHocVansByPageCommand : ICrudCommand
    {
        private readonly ITrinhDoHocVanService _service;
        private readonly int _page;
        private readonly int _pageSize;

        public GetTrinhDoHocVansByPageCommand(ITrinhDoHocVanService service, int page = 1, int pageSize = 10)
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
