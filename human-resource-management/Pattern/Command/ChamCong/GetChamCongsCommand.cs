using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChamCongService;

namespace HumanResourceManagement.Pattern.Command.ChamCong
{
    public class GetChamCongsCommand : ICrudCommand
    {
        private readonly IChamCongService chamCongService;

        public GetChamCongsCommand(IChamCongService chamCongService)
        {
            this.chamCongService = chamCongService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> chamCongs = await chamCongService.GetChamCongs();

            return new ApiResponse()
            {
                Data = chamCongs,
                Message = "Lấy thông tin chấm công thành công",
                StatusCode = 200
            };
        }
    }
}
