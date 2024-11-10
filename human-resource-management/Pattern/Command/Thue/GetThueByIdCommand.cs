using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThueService;

namespace HumanResourceManagement.Pattern.Command.Thue
{
    public class GetThueByIdCommand : ICrudCommand
    {
        private readonly IThueService thueService;
        private readonly string id;

        public GetThueByIdCommand(IThueService thueService, string id)
        {
            this.thueService = thueService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            object thue = await thueService.GetThueById(id);

            return new ApiResponse()
            {
                Data = thue,
                Message = "Lấy thông tin thuế thành công",
                StatusCode = 200
            };
        }
    }
}
