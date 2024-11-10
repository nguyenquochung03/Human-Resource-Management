using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public interface IPhuCapCommand
    {
        public Task<ApiResponse> Execute();
    }
}
