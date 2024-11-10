using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.TaiKhoan
{
    public interface ITaiKhoanCommand
    {
        public Task<ApiResponse> Execute();
    }
}
