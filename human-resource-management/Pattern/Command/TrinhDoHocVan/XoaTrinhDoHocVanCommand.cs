using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TrinhDoHocVanService;

namespace HumanResourceManagement.Pattern.Command.TrinhDoHocVan
{
    public class XoaTrinhDoHocVanCommand : ICrudCommand
    {
        private readonly ITrinhDoHocVanService _trinhDoHocVanService;
        private readonly string _id;

        public XoaTrinhDoHocVanCommand(ITrinhDoHocVanService trinhDoHocVanService, string id)
        {
            _trinhDoHocVanService = trinhDoHocVanService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _trinhDoHocVanService.XoaTrinhDoHocVan(_id);

            return new ApiResponse()
            {
                Message = "Xóa trình độ học vấn thành công",
                StatusCode = 204
            };
        }
    }
}
