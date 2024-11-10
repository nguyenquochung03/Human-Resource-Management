using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class GetLichLamNhanViensCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;

        public GetLichLamNhanViensCommand(ILichLamService lichLamService)
        {
            this.lichLamService = lichLamService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<Models.LichLamNhanVien> lichLamNhanViens = await lichLamService.GetLichLamNhanViens();

            return new ApiResponse()
            {
                Data = lichLamNhanViens,
                Message = "Lấy thông tin lịch làm nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
