using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class ThemLichLamNhanVienCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;
        private readonly CDLichLamForNhanVienDTO cDLichLamForNhanVienDTO;

        public ThemLichLamNhanVienCommand(ILichLamService lichLamService, CDLichLamForNhanVienDTO cDLichLamForNhanVienDTO)
        {
            this.lichLamService = lichLamService;
            this.cDLichLamForNhanVienDTO = cDLichLamForNhanVienDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            await lichLamService.ThemLichLamNhanVien(cDLichLamForNhanVienDTO);

            return new ApiResponse()
            {
                Message = "Thêm lịch làm cho nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
