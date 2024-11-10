using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class XoaLichLamCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;
        private readonly string id;

        public XoaLichLamCommand(ILichLamService lichLamService, string id)
        {
            this.lichLamService = lichLamService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await lichLamService.XoaLichLam(id);

            return new ApiResponse()
            {
                Message = "Xóa lịch làm thành công",
                StatusCode = 204
            };
        }
    }
}
