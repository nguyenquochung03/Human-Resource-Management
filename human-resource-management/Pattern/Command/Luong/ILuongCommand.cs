using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.Luong
{
    public interface ILuongCommand
    {
        public Task<ApiResponse> Execute();
    }
}
