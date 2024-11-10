using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NghiVangService;

namespace HumanResourceManagement.Pattern.Command.NghiVang
{
    public class ThemNghiVangCommand : ICrudCommand
    {
        private readonly INghiVangService nghiVangService;
        private readonly NghiVangDTO nghiVangDTO;

        public ThemNghiVangCommand(INghiVangService nghiVangService, NghiVangDTO nghiVangDTO)
        {
            this.nghiVangService = nghiVangService;
            this.nghiVangDTO = nghiVangDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object nghiVang = await nghiVangService.ThemNghiVang(nghiVangDTO);

            return new ApiResponse()
            {
                Data = nghiVang,
                Message = "Thêm nghỉ vắng nhân viên thành công",
                StatusCode = 201
            };
        }
    }
}
