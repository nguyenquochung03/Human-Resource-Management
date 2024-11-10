using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChamCongService;

namespace HumanResourceManagement.Pattern.Command.ChamCong
{
    public class ThemChamCongNhanVienCommand : ICrudCommand
    {
        private readonly IChamCongService chamCongService;
        private readonly ChamCongDTO chamCongDTO;

        public ThemChamCongNhanVienCommand(IChamCongService chamCongService, ChamCongDTO chamCongDTO)
        {
            this.chamCongService = chamCongService;
            this.chamCongDTO = chamCongDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object chamCong = await chamCongService.ThemChamCong(chamCongDTO);

            return new ApiResponse()
            {
                Data = chamCong,
                Message = "Thêm chấm công nhân viên thành công",
                StatusCode = 201
            };
        }
    }
}
