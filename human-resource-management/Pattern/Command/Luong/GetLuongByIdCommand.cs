using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LuongService;

namespace HumanResourceManagement.Pattern.Command.Luong
{
    public class GetLuongByIdCommand : ICrudCommand
    {
        private readonly ILuongService _luongService;
        private readonly string _id;

        public GetLuongByIdCommand(ILuongService luongService, string id)
        {
            _luongService = luongService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            object luong = await _luongService.GetLuongById(_id);

            return new ApiResponse()
            {
                Data = luong,
                Message = "Lấy thông tin lương thành công",
                StatusCode = 200
            };
        }
    }
}
