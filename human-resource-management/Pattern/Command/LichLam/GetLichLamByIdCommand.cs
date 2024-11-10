using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class GetLichLamByIdCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;
        private readonly string id;

        public GetLichLamByIdCommand(ILichLamService lichLamService, string id)
        {
            this.lichLamService = lichLamService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            object lichLam = await lichLamService.GetLichLamById(id);

            return new ApiResponse()
            {
                Data = lichLam,
                Message = "Lấy thông tin lịch làm thành công",
                StatusCode = 200
            };
        }
    }
}
