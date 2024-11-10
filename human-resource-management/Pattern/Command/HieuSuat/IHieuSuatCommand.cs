using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.HieuSuat
{
    public interface IHieuSuatCommand
    {
        public Task<ApiResponse> Execute();
    }
}
