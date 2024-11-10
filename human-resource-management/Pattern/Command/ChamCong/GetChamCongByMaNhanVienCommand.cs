using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChamCongService;

namespace HumanResourceManagement.Pattern.Command.ChamCong
{
    public class GetChamCongByMaNhanVienCommand : ICrudCommand
    {
        private readonly IChamCongService chamCongService;
        private readonly string maNhanVien;

        public GetChamCongByMaNhanVienCommand(IChamCongService chamCongService, string maNhanVien)
        {
            this.chamCongService = chamCongService;
            this.maNhanVien = maNhanVien;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> chamCongs = await chamCongService.GetChamCongByMaNhanVien(maNhanVien);

            return new ApiResponse()
            {
                Data = chamCongs,
                Message = "Lấy thông tin chấm công thành công",
                StatusCode = 200
            };
        }
    }
}
