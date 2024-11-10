using HumanResourceManagement.Response;

namespace HumanResourceManagement.Pattern.Command.TrinhDoHocVan
{
    public interface ITrinhDoHocVanCommand
    {
        public Task<ApiResponse> Execute();
    }
}
