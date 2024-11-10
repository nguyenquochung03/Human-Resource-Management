using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class GetPhuCapsByPageCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;
        private readonly int _page;
        private readonly int _pageSize;

        public GetPhuCapsByPageCommand(MyPhuCapsService myPhuCapsService, int page = 1, int pageSize = 10)
        {
            this.myPhuCapsService = myPhuCapsService;
            _page = page;
            _pageSize = pageSize;
        }

        public async Task<ApiResponse> Execute()
        {
            var data = myPhuCapsService.GetListByPage(_page, _pageSize);
            return new ApiResponse
            {
                Data = data,
                Message = "Success",
                StatusCode = 200
            };
        }
    }
}
