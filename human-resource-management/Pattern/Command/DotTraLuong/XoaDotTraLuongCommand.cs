using HumanResourceManagement.Response;
using HumanResourceManagement.Services.DotTraLuongService;

namespace HumanResourceManagement.Pattern.Command.DotTraLuong
{
    public class XoaDotTraLuongCommand : ICrudCommand
    {
        private readonly IDotTraLuongService dotTraLuongService;
        private readonly string id;

        public XoaDotTraLuongCommand(IDotTraLuongService dotTraLuongService, string id)
        {
            this.dotTraLuongService = dotTraLuongService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await dotTraLuongService.XoaDotTraLuong(id);

            return new ApiResponse()
            {
                Message = "Xóa đợt trả lương thành công",
                StatusCode = 204
            };
        }
    }
}
