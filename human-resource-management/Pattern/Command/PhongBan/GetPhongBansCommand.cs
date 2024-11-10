using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhongBanService;

namespace HumanResourceManagement.Pattern.Command.PhongBan
{
    public class GetPhongBansCommand : ICrudCommand
    {
        private readonly IPhongBanService _phongBanService;

        public GetPhongBansCommand(IPhongBanService phongBanService)
        {
            _phongBanService = phongBanService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<Models.PhongBan> phongBans = await _phongBanService.GetPhongBans();
            return new ApiResponse()
            {
                Data = phongBans,
                Message = "Lấy thông tin phòng ban thành công",
                StatusCode = 200
            };
        }
    }
}
