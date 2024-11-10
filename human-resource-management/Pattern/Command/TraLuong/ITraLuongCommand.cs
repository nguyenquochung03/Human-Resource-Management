using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.TraLuong
{
    public interface ITraLuongCommand
    {
        public Task<ApiResponse> Execute();
    }
}
