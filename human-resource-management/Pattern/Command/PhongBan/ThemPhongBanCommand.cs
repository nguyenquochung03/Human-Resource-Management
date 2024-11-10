using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhongBanService;

namespace HumanResourceManagement.Pattern.Command.PhongBan
{
    public class ThemPhongBanCommand : ICrudCommand
    {
        private readonly IPhongBanService _phongBanService;
        private readonly PhongBanDTO _phongBanDto;

        public ThemPhongBanCommand(IPhongBanService phongBanService, PhongBanDTO phongBanDto)
        {
            _phongBanService = phongBanService;
            _phongBanDto = phongBanDto;
        }

        public async Task<ApiResponse> Execute()
        {
            Models.PhongBan phongBan = await _phongBanService.ThemPhongBan(_phongBanDto);
            return new ApiResponse()
            {
                StatusCode = 201,
                Data = phongBan,
                Message = "Thêm phòng ban thành công"
            };
        }
    }
}
