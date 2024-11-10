using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChamCongService;

namespace HumanResourceManagement.Pattern.Command.ChamCong
{
    public class XoaChamCongCommand : ICrudCommand
    {
        private readonly IChamCongService chamCongService;
        private readonly string id;

        public XoaChamCongCommand(IChamCongService chamCongService, string id)
        {
            this.chamCongService = chamCongService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await chamCongService.XoaChamCong(id);

            return new ApiResponse()
            {
                Message = "Xóa chấm công nhân viên thành công",
                StatusCode = 204
            };
        }
    }
}
