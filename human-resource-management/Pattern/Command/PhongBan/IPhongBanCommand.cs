using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.PhongBan
{
    public interface IPhongBanCommand
    {
        public Task<ApiResponse> Execute();
    }
}
