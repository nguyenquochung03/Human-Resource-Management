using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TaiKhoanService;

namespace HumanResourceManagement.Pattern.Command.TaiKhoan
{
    public class DangKiCommand : ICrudCommand
    {
        private readonly ITaiKhoanService taiKhoanService;
        private readonly DangKiDTO taiKhoanDTO;

        public DangKiCommand(ITaiKhoanService taiKhoanService, DangKiDTO taiKhoanDTO)
        {
            this.taiKhoanService = taiKhoanService;
            this.taiKhoanDTO = taiKhoanDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object response = await taiKhoanService.DangKi(taiKhoanDTO);
            return new ApiResponse()
            {
                Data = response,
                Message = "Đăng kí tài khoản thành công",
                StatusCode = 201
            };
        }
    }
}
