using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class ThemLichLamCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;
        private readonly LichLamDTO lichLamDTO;

        public ThemLichLamCommand(ILichLamService lichLamService, LichLamDTO lichLamDTO)
        {
            this.lichLamService = lichLamService;
            this.lichLamDTO = lichLamDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object lichLam = await lichLamService.ThemLichLam(lichLamDTO);

            return new ApiResponse()
            {
                Data = lichLam,
                Message = "Thêm lịch làm thành công",
                StatusCode = 201
            };
        }
    }
}
