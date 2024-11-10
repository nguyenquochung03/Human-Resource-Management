using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class GetPhuCapNhanViensCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;

        public GetPhuCapNhanViensCommand(MyPhuCapsService myPhuCapsService)
        {
            this.myPhuCapsService = myPhuCapsService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<Models.PhuCapNhanVien> phuCapNhanViens = await myPhuCapsService.GetPhuCapNhanViens();

            return new ApiResponse()
            {
                Data = phuCapNhanViens,
                Message = "Lấy phụ cấp nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
