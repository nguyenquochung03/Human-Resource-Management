using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HopDongService;

namespace HumanResourceManagement.Pattern.Command.HopDong
{
    public class ThemHopDongCommand : ICrudCommand
    {
        private readonly IHopDongService _hopDongService;
        private readonly HopDongDTO _hopDongDTO;

        public ThemHopDongCommand(IHopDongService hopDongService, HopDongDTO hopDongDTO)
        {
            _hopDongService = hopDongService;
            _hopDongDTO = hopDongDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object hopDong = await _hopDongService.ThemHopDong(_hopDongDTO);

            return new ApiResponse()
            {
                Data = hopDong,
                Message = "Thêm hợp đồng thành công",
                StatusCode = 201
            };
        }
    }
}
