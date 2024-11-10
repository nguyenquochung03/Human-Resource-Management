using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class GetLichLamsCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;

        public GetLichLamsCommand(ILichLamService lichLamService)
        {
            this.lichLamService = lichLamService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> lichLams = await lichLamService.GetLichLams();

            return new ApiResponse()
            {
                Data = lichLams,
                Message = "Lấy thông tin lịch làm thành công",
                StatusCode = 200
            };
        }
    }
}
