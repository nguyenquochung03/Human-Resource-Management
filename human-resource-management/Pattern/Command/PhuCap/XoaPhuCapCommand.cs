using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class XoaPhuCapCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;
        private string id;

        public XoaPhuCapCommand(MyPhuCapsService myPhuCapsService, string id)
        {
            this.myPhuCapsService = myPhuCapsService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await myPhuCapsService.XoaPhuCap(id);
            return new ApiResponse()
            {
                Message = "Xóa thông tin phụ cấp thành công",
                StatusCode = 204
            };
        }
    }
}
