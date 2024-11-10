using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class SuaPhuCapCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;
        private PhuCapDTO phuCap;
        private string id;

        public SuaPhuCapCommand(MyPhuCapsService myPhuCapsService, PhuCapDTO phuCap, string id)
        {
            this.myPhuCapsService = myPhuCapsService;
            this.phuCap = phuCap;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            if (await myPhuCapsService.SuaPhuCap(id, phuCap))
            {
                return new ApiResponse()
                {
                    Message = "Cập nhật thông tin phụ cấp thành công",
                    StatusCode = 204
                };
            }
            return new ApiResponse()
            {
                Message = "Cập nhật thông tin phụ cấp thất bại",
                StatusCode = 205
            };
        }
    }
}
