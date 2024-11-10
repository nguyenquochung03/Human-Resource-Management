using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class GetThuongByIdCommand : ICrudCommand
    {
        private readonly IThuongService thuongService;
        private readonly string id;

        public GetThuongByIdCommand(IThuongService thuongService, string id)
        {
            this.thuongService = thuongService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            object thuong = await thuongService.GetThuongById(id);

            return new ApiResponse()
            {
                Data = thuong,
                Message = "Lấy thông tin thưởng thành công",
                StatusCode = 200
            };
        }
    }
}
