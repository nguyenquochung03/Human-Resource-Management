using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LuongService;

namespace HumanResourceManagement.Pattern.Command.Luong
{
    public class GetLuongsCommand : ICrudCommand
    {
        private readonly ILuongService luongService;

        public GetLuongsCommand(ILuongService luongService)
        {
            this.luongService = luongService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> luongs = await luongService.GetLuongs();

            return new ApiResponse()
            {
                Data = luongs,
                Message = "Lấy thông tin lương thành công",
                StatusCode = 200
            };
        }
    }
}
