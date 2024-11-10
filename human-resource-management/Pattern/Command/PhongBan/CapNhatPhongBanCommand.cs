using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhongBanService;

namespace HumanResourceManagement.Pattern.Command.PhongBan
{
    public class CapNhatPhongBanCommand : ICrudCommand
    {
        private readonly IPhongBanService _phongBanService;
        private readonly PhongBanDTO _phongBanDto;
        private readonly string _id;

        public CapNhatPhongBanCommand(IPhongBanService phongBanService, PhongBanDTO phongBanDto, string id)
        {
            _phongBanService = phongBanService;
            _phongBanDto = phongBanDto;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _phongBanService.SuaPhongBan(_id, _phongBanDto);
            return new ApiResponse()
            {
                Message = "Cập nhật thông tin phòng ban thành công",
                StatusCode = 204
            };
        }
    }
}
