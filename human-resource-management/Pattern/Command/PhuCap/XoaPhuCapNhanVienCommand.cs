using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class XoaPhuCapNhanVienCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;
        private readonly CDPhuCapForNhanVienDTO _cDPhuCapForNhanVienDTO;

        public XoaPhuCapNhanVienCommand(MyPhuCapsService myPhuCapsService, CDPhuCapForNhanVienDTO cDPhuCapForNhanVienDTO)
        {
            this.myPhuCapsService = myPhuCapsService;
            _cDPhuCapForNhanVienDTO = cDPhuCapForNhanVienDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            if (await myPhuCapsService.XoaPhuCapForNvs(_cDPhuCapForNhanVienDTO))
            {
                return new ApiResponse()
                {
                    Message = "Xóa phụ cấp nhân viên thành công",
                    Data = 204
                };
            }

            return new ApiResponse()
            {
                Message = "Xóa nhân viên thất bại",
                Data = 204
            };
        }
    }
}
