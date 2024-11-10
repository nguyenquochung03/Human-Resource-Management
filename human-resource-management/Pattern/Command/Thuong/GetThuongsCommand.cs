using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class GetThuongsCommand : ICrudCommand
    {
        private readonly IThuongService thuongService;

        public GetThuongsCommand(IThuongService thuongService)
        {
            this.thuongService = thuongService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> thuongs = await thuongService.GetThuongs();

            return new ApiResponse()
            {
                Data = thuongs,
                Message = "Lấy thông tin thưởng thành công",
                StatusCode = 200,
            };
        }
    }
}
