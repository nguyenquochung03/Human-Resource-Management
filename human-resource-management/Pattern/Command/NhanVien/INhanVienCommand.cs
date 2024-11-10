using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public interface INhanVienCommand
    {
        public Task<ApiResponse> Execute();
    }
}
