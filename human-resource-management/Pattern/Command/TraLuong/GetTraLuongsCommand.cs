using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TraLuongService;

namespace HumanResourceManagement.Pattern.Command.TraLuong
{
    public class GetTraLuongsCommand : ICrudCommand
    {
        private readonly ITraLuongService traLuongService;

        public GetTraLuongsCommand(ITraLuongService traLuongService)
        {
            this.traLuongService = traLuongService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> traLuongs = await traLuongService.GetTraLuongs();

            return new ApiResponse()
            {
                Data = traLuongs,
                Message = "Lấy thông tin trả lương thành công",
                StatusCode = 200
            };
        }
    }
}
