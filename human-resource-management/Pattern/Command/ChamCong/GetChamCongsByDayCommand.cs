using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChamCongService;

namespace HumanResourceManagement.Pattern.Command.ChamCong
{
    public class GetChamCongsByDayCommand : ICrudCommand
    {
        private readonly IChamCongService chamCongService;
        private readonly DateTime day;

        public GetChamCongsByDayCommand(IChamCongService chamCongService, DateTime day)
        {
            this.chamCongService = chamCongService;
            this.day = day;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> chamCongs = await chamCongService.GetChamCongsByDay(day);

            return new ApiResponse()
            {
                Data = chamCongs,
                Message = "Lấy thông tin chấm công thành công",
                StatusCode = 200
            };
        }
    }
}
