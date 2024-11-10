using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThueService;

namespace HumanResourceManagement.Pattern.Command.Thue
{
    public class GetThueCommand : ICrudCommand
    {
        private readonly IThueService thueService;

        public GetThueCommand(IThueService thueService)
        {
            this.thueService = thueService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> thues = await thueService.GetThues();

            return new ApiResponse()
            {
                Data = thues,
                Message = "Lấy thông tin thuế thành công",
                StatusCode = 200
            };
        }
    }
}
