using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class AddPhuCapForNvCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;
        private readonly CDPhuCapForNhanVienDTO dto;

        public AddPhuCapForNvCommand(MyPhuCapsService myPhuCapsService, CDPhuCapForNhanVienDTO dto)
        {
            this.myPhuCapsService = myPhuCapsService;
            this.dto = dto;
        }
        public async Task<ApiResponse> Execute()
        {
            object phuCap = await myPhuCapsService.AddPhuCapForNvs(dto);
            return new ApiResponse()
            {
                Data = "No content",
                Message = "Thêm thông tin phụ cấp cho thành viên thành công",
                StatusCode = 200
            };
        }
    }
}
