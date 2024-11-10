using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NhanVienService;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public class GetNhanVienByPhoneCommand : ICrudCommand
    {
        private readonly INhanVienService _nhanVienService;
        private readonly string phone;

        public GetNhanVienByPhoneCommand(INhanVienService nhanVienService, string phone)
        {
            _nhanVienService = nhanVienService;
            this.phone = phone;
        }

        public async Task<ApiResponse> Execute()
        {
            object nhanVien = await _nhanVienService.GetNhanVienByPhone(this.phone);

            return new ApiResponse()
            {
                Data = nhanVien,
                Message = "Lấy nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
