using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HieuSuatService;

namespace HumanResourceManagement.Pattern.Command.HieuSuat
{
    public class GetHieuSuatsCommand : ICrudCommand
    {
        private readonly IHieuSuatService hieuSuatService;

        public GetHieuSuatsCommand(IHieuSuatService hieuSuatService)
        {
            this.hieuSuatService = hieuSuatService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> hieuSuats = await hieuSuatService.GetHieuSuats();

            return new ApiResponse()
            {
                Data = hieuSuats,
                Message = "Lấy thông tin hiệu suất thành công",
                StatusCode = 200
            };
        }
    }
}
