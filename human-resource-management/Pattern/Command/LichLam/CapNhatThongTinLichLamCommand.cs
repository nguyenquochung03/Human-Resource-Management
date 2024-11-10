using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class CapNhatThongTinLichLamCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;
        private readonly LichLamDTO lichLamDTO;
        private readonly string id;

        public CapNhatThongTinLichLamCommand(ILichLamService lichLamService, LichLamDTO lichLamDTO, string id)
        {
            this.lichLamService = lichLamService;
            this.lichLamDTO = lichLamDTO;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await lichLamService.CapNhatLichLam(id, lichLamDTO);

            return new ApiResponse()
            {
                Message = "Cập nhật thông tin lịch làm thành công",
                StatusCode = 204
            };
        }
    }
}
