using HumanResourceManagement.Response;
using HumanResourceManagement.Services.DotTraLuongService;

namespace HumanResourceManagement.Pattern.Command.DotTraLuong
{
    public class GetDotTraLuongsCommand : ICrudCommand
    {
        private readonly IDotTraLuongService dotTraLuongService;

        public GetDotTraLuongsCommand(IDotTraLuongService dotTraLuongService)
        {
            this.dotTraLuongService = dotTraLuongService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> dotTraLuongs = await dotTraLuongService.GetDotTraLuongs();

            return new ApiResponse()
            {
                StatusCode = 200,
                Data = dotTraLuongs,
                Message = "Lấy thông đợt trả lương thành công"
            };
        }
    }
}
