using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HopDongService;

namespace HumanResourceManagement.Pattern.Command.HopDong
{
    public class GetHopDongsCommand : ICrudCommand
    {
        private readonly IHopDongService _hopDongService;

        public GetHopDongsCommand(IHopDongService hopDongService)
        {
            _hopDongService = hopDongService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> hopDongs = await _hopDongService.GetHopDongs();

            return new ApiResponse()
            {
                Data = hopDongs,
                Message = "Lấy thông tin hợp đồng thành công",
                StatusCode = 200
            };
        }
    }
}
