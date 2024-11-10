using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TaiKhoanService;

namespace HumanResourceManagement.Pattern.Command.TaiKhoan
{
    public class GetPrincipalCommand : ICrudCommand
    {
        private readonly ITaiKhoanService taiKhoanService;
        private readonly string username;

        public GetPrincipalCommand(ITaiKhoanService taiKhoanService, string username)
        {
            this.taiKhoanService = taiKhoanService;
            this.username = username;
        }

        public async Task<ApiResponse> Execute()
        {
            object response = await taiKhoanService.GetPrincipal(username);
            return new ApiResponse()
            {
                Data = response,
                Message = "Lấy thông tin người dùng thành công",
                StatusCode = 200
            };
        }
    }
}
