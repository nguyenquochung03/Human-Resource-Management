using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.Thue
{
    public interface IThueCommand
    {
        public Task<ApiResponse> Execute();
    }
}
