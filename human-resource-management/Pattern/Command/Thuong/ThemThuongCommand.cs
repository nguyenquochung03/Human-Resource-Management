using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class ThemThuongCommand : ICrudCommand
    {
        private readonly IThuongService thuongService;
        private readonly ThuongDTO thuongDTO;

        public ThemThuongCommand(IThuongService thuongService, ThuongDTO thuongDTO)
        {
            this.thuongService = thuongService;
            this.thuongDTO = thuongDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object thuong = await thuongService.ThemThuong(thuongDTO);

            return new ApiResponse()
            {
                Data = thuong,
                Message = "Thêm thưởng thành công",
                StatusCode = 201
            };
        }
    }
}
