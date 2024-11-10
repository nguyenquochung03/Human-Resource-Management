using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class GetLichLamByMaNhanVienCommand : ICrudCommand
    {
        private readonly ILichLamService lichLamService;
        private readonly string maNhanVien;

        public GetLichLamByMaNhanVienCommand(ILichLamService lichLamService, string maNhanVien)
        {
            this.lichLamService = lichLamService;
            this.maNhanVien = maNhanVien;
        }

        public async Task<ApiResponse> Execute()
        {
            object lichLam = await lichLamService.GetLichLamByMaNhanVien(maNhanVien);

            return new ApiResponse()
            {
                Data = lichLam,
                Message = "Lấy thông tin lịch làm thành công",
                StatusCode = 200
            };
        }
    }
}
