using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class XoaLichLamNhanVienCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;
        private readonly CDLichLamForNhanVienDTO cDLichLamForNhanVienDTO;

        public XoaLichLamNhanVienCommand(ILichLamService lichLamService, CDLichLamForNhanVienDTO cDLichLamForNhanVienDTO)
        {
            this.lichLamService = lichLamService;
            this.cDLichLamForNhanVienDTO = cDLichLamForNhanVienDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            await lichLamService.XoaLichLamNhanVien(cDLichLamForNhanVienDTO);

            return new ApiResponse()
            {
                Message = "Xóa lịch làm cho nhân viên thành công",
                StatusCode = 204
            };
        }
    }
}
