using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class XoaThuongCommand : ICrudCommand
    {
        private readonly IThuongService thuongService;
        private readonly string id;

        public XoaThuongCommand(IThuongService thuongService, string id)
        {
            this.thuongService = thuongService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await thuongService.XoaThuong(id);

            return new ApiResponse()
            {
                Message = "Xóa thưởng thành công",
                StatusCode = 204
            };
        }
    }
}
