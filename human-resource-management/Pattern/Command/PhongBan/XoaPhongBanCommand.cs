using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhongBanService;

namespace HumanResourceManagement.Pattern.Command.PhongBan
{
    public class XoaPhongBanCommand : ICrudCommand
    {
        private readonly IPhongBanService _phongBanService;
        private readonly string _id;

        public XoaPhongBanCommand(IPhongBanService phongBanService, string id)
        {
            _phongBanService = phongBanService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _phongBanService.XoaPhongBan(_id);
            return new ApiResponse
            {
                StatusCode = 204,
                Message = "Xóa phòng ban thành công"
            };
        }
    }
}
