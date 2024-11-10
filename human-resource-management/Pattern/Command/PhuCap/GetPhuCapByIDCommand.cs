using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class GetPhuCapByIDCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;
        private readonly string phuCapId;

        public GetPhuCapByIDCommand(MyPhuCapsService myPhuCapsService, string phuCapId)
        {
            this.myPhuCapsService = myPhuCapsService;
            this.phuCapId = phuCapId;
        }

        public async Task<ApiResponse> Execute()
        {
            object phuCap = await myPhuCapsService.GetPhuCapById(phuCapId);
            return new ApiResponse()
            {
                Data = phuCap,
                Message = "Lấy thông tin phụ cấp thành công",
                StatusCode = 200
            };

        }
    }
}
