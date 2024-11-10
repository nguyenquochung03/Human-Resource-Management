using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.DotTraLuong
{
    public interface IDotTraLuongCommand
    {
        public Task<ApiResponse> Execute();
    }
}
