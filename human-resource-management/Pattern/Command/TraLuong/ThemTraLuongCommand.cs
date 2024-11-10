using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TraLuongService;

namespace HumanResourceManagement.Pattern.Command.TraLuong
{
    public class ThemTraLuongCommand : ICrudCommand
    {
        private readonly ITraLuongService traLuongService;
        private readonly TraLuongDTO traLuongDTO;

        public ThemTraLuongCommand(ITraLuongService traLuongService, TraLuongDTO traLuongDTO)
        {
            this.traLuongService = traLuongService;
            this.traLuongDTO = traLuongDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object traLuong = await traLuongService.ThemTraLuong(traLuongDTO);

            return new ApiResponse()
            {
                Data = traLuong,
                Message = "Thêm trả lương nhân viên thành công",
                StatusCode = 201
            };
        }
    }
}
