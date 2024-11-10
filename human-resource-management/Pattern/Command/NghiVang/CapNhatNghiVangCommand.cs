using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NghiVangService;

namespace HumanResourceManagement.Pattern.Command.NghiVang
{
    public class CapNhatNghiVangCommand : ICrudCommand
    {
        private readonly INghiVangService nghiVangService;
        private readonly NghiVangDTO nghiVangDTO;
        private readonly string id;

        public CapNhatNghiVangCommand(INghiVangService nghiVangService, NghiVangDTO nghiVangDTO, string id)
        {
            this.nghiVangService = nghiVangService;
            this.nghiVangDTO = nghiVangDTO;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await nghiVangService.CapNhatNghiVang(id, nghiVangDTO);

            return new ApiResponse()
            {
                Message = "Cập nhật nghỉ vắng nhân viên thành công",
                StatusCode = 204
            };
        }
    }
}
