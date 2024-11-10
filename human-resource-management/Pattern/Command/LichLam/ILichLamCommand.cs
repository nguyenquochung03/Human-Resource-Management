using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public interface ILichLamCommand
    {
        public Task<ApiResponse> Execute();
    }
}
