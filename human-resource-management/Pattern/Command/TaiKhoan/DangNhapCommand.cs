using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TaiKhoanService;

namespace HumanResourceManagement.Pattern.Command.TaiKhoan
{
    public class DangNhapCommand : ICrudCommand
    {
        private readonly ITaiKhoanService taiKhoanService;
        private readonly TaiKhoanDTO taiKhoanDTO;

        public DangNhapCommand(ITaiKhoanService taiKhoanService, TaiKhoanDTO taiKhoanDTO)
        {
            this.taiKhoanService = taiKhoanService;
            this.taiKhoanDTO = taiKhoanDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object response = await taiKhoanService.DangNhap(taiKhoanDTO);
            return new ApiResponse()
            {
                Data = response,
                Message = "Đăng nhập thành công",
                StatusCode = 200
            };
        }
    }
}
