using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HopDongService;

namespace HumanResourceManagement.Pattern.Command.HopDong
{
    public class XoaHopDongCommand : ICrudCommand
    {
        private readonly IHopDongService _hopDongService;
        private readonly string _id;

        public XoaHopDongCommand(IHopDongService hopDongService, string id)
        {
            _hopDongService = hopDongService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _hopDongService.XoaHopDong(_id);

            return new ApiResponse()
            {
                Message = "Xóa hợp đồng thành công",
                StatusCode = 204
            };
        }
    }
}
