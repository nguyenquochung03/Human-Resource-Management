using HumanResourceManagement.Response;
using HumanResourceManagement.Services.DotTraLuongService;

namespace HumanResourceManagement.Pattern.Command.DotTraLuong
{
    public class GetDotTraLuongByIdCommand : ICrudCommand
    {
        private readonly IDotTraLuongService _dotTraLuongService;
        private readonly string id;

        public GetDotTraLuongByIdCommand(IDotTraLuongService dotTraLuongService, string id)
        {
            _dotTraLuongService = dotTraLuongService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            object dotTraLuong = await _dotTraLuongService.GetDotTraLuongById(id);

            return new ApiResponse()
            {
                Data = dotTraLuong,
                Message = "Lấy thông tin đợt trả lương thành công",
                StatusCode = 200
            };
        }
    }
}
