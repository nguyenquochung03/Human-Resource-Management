using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChamCongService;

namespace HumanResourceManagement.Pattern.Command.ChamCong
{
    public class SuaChamCongCommand : ICrudCommand
    {
        private readonly IChamCongService chamCongService;
        private readonly ChamCongDTO chamCongDTO;
        private readonly string id;

        public SuaChamCongCommand(IChamCongService chamCongService, ChamCongDTO chamCongDTO, string id)
        {
            this.chamCongService = chamCongService;
            this.chamCongDTO = chamCongDTO;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await chamCongService.SuaChamCong(id, chamCongDTO);

            return new ApiResponse()
            {
                Message = "Cập nhật thông tin chấm công thành công",
                StatusCode = 204
            };
        }
    }
}
