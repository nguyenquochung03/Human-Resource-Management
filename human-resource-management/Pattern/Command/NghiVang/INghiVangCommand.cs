using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.NghiVang
{
    public interface INghiVangCommand
    {
        public Task<ApiResponse> Execute();
    }
}
