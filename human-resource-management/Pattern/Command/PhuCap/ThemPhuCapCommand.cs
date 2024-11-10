using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class ThemPhuCapCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;
        private PhuCapDTO phuCap;

        public ThemPhuCapCommand(MyPhuCapsService myPhuCapsService, PhuCapDTO phuCapDTO)
        {
            this.phuCap = phuCapDTO;
            this.myPhuCapsService = myPhuCapsService;
        }

        public async Task<ApiResponse> Execute()
        {
            object newPhuCap = await myPhuCapsService.ThemPhuCap(phuCap);
            return new ApiResponse()
            {
                StatusCode = 201,
                Data = newPhuCap,
                Message = "Thêm phụ cấp thành công"
            };
        }
    }
}
