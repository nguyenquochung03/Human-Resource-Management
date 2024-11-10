using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.HopDong
{
    public interface IHopDongCommand
    {
        public Task<ApiResponse> Execute();
    }
}
