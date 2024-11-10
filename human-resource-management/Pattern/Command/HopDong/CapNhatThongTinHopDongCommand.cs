using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HopDongService;

namespace HumanResourceManagement.Pattern.Command.HopDong
{
    public class CapNhatThongTinHopDongCommand : ICrudCommand
    {
        private readonly IHopDongService _hopDongService;
        private readonly HopDongDTO _hopDongDTO;
        private readonly string _id;

        public CapNhatThongTinHopDongCommand(IHopDongService hopDongService, HopDongDTO hopDongDTO, string id)
        {
            _hopDongService = hopDongService;
            _hopDongDTO = hopDongDTO;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _hopDongService.CapNhatHopDong(_id, _hopDongDTO);

            return new ApiResponse()
            {
                Message = "Cập nhật thông tin hợp đồng thành công",
                StatusCode = 204
            };
        }
    }
}
