using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class CapNhatThongTinThuongCommand : ICrudCommand
    {
        private readonly IThuongService thuongService;
        private readonly ThuongDTO thuongDTO;
        private readonly string id;

        public CapNhatThongTinThuongCommand(IThuongService thuongService, ThuongDTO thuongDTO, string id)
        {
            this.thuongService = thuongService;
            this.thuongDTO = thuongDTO;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await thuongService.CapNhatThongTinThuong(id, thuongDTO);

            return new ApiResponse()
            {
                Data = thuongDTO,
                Message = "Cập nhật thông tin thưởng thành công",
                StatusCode = 204
            };
        }
    }
}
