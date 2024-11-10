using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TrinhDoHocVanService;

namespace HumanResourceManagement.Pattern.Command.TrinhDoHocVan
{
    public class CapNhatTrinhDoHocVanCommand : ICrudCommand
    {
        private readonly ITrinhDoHocVanTargetDTO _adapter;
        private readonly TrinhDoHocVanDTO _trinhDoHocVanDTO;
        private readonly string _id;

        public CapNhatTrinhDoHocVanCommand(ITrinhDoHocVanTargetDTO adapter, TrinhDoHocVanDTO trinhDoHocVanDTO, string id)
        {
            _adapter = adapter;
            _trinhDoHocVanDTO = trinhDoHocVanDTO;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _adapter.SuaTrinhDoHocVan(_id, _trinhDoHocVanDTO);

            return new ApiResponse()
            {
                Message = "Cập nhật trình độ học vấn thành công",
                StatusCode = 204
            };
        }
    }
}
