using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChamCongService;

namespace HumanResourceManagement.Pattern.Command.ChamCong
{
    public class GetChamCongsFromToCommand : ICrudCommand
    {
        private readonly IChamCongService chamCongService;
        private string employeeId;
        private DateTime from;
        private DateTime to;

        public GetChamCongsFromToCommand(IChamCongService chamCongService, string employeeId, DateTime from, DateTime to)
        {
            this.chamCongService = chamCongService;
            this.employeeId = employeeId;
            this.from = from;
            this.to = to;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> chamCongs = await chamCongService.GetChamCongsFromTo(from, to, employeeId);

            return new ApiResponse()
            {
                Data = chamCongs,
                Message = "Lấy thông tin chấm công thành công",
                StatusCode = 200
            };
        }
    }
}
