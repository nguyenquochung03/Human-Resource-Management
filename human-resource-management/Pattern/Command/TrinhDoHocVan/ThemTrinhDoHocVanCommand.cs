using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter;
using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.TrinhDoHocVan
{
    public class ThemTrinhDoHocVanCommand : ICrudCommand
    {
        private readonly ITrinhDoHocVanTargetDTO _adapter;
        private readonly TrinhDoHocVanDTO trinhDoHocVanDTO;

        public ThemTrinhDoHocVanCommand(ITrinhDoHocVanTargetDTO adapter, TrinhDoHocVanDTO trinhDoHocVanDTO)
        {
            _adapter = adapter;
            this.trinhDoHocVanDTO = trinhDoHocVanDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object trinhDoHocVan = await _adapter.ThemTrinhDoHocVan(trinhDoHocVanDTO);

            return new ApiResponse()
            {
                Data = trinhDoHocVan,
                Message = "Thêm trình độ học vấn thành công",
                StatusCode = 201
            };
        }
    }
}
