using HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TrinhDoHocVanService;

namespace HumanResourceManagement.Pattern.Command.TrinhDoHocVan
{
    public class GetTrinhDoHocVansCommand : ICrudCommand
    {
        private readonly ITrinhDoHocVanService _trinhDoHocVanService;

        public GetTrinhDoHocVansCommand(ITrinhDoHocVanService trinhDoHocVanService)
        {
            _trinhDoHocVanService = trinhDoHocVanService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> trinhDoHocVans = await _trinhDoHocVanService.GetTrinhDoHocVan();

            return new ApiResponse()
            {
                Data = trinhDoHocVans,
                Message = "Lấy trình độ học vấn thành công",
                StatusCode = 200
            };
        }
    }
}
