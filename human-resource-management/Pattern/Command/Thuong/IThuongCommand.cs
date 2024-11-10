using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public interface IThuongCommand
    {
        public Task<ApiResponse> Execute();
    }
}
