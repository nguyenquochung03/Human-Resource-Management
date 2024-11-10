using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class GetPhuCapsCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;

        public GetPhuCapsCommand(MyPhuCapsService myPhuCapsService)
        {
            this.myPhuCapsService = myPhuCapsService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> phuCaps = await myPhuCapsService.GetPhuCaps();
            return new ApiResponse()
            {
                Data = phuCaps,
                Message = "Lấy thông tin phụ cấp thành công",
                StatusCode = 200
            };
        }
    }
}
