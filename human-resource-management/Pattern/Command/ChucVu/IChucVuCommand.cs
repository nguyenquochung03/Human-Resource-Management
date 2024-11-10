using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.ChucVu
{
    public interface IChucVuCommand
    {
        public Task<ApiResponse> Execute();
    }
}
