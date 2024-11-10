using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command
{
    public interface ICrudCommand
    {
        public Task<ApiResponse> Execute();
    }
}
