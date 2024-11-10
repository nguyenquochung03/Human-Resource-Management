using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class GetThuongByMaNhanVienCommand : ICrudCommand
    {
        private readonly IThuongService thuongService;
        private readonly string maNhanVien;

        public GetThuongByMaNhanVienCommand(IThuongService thuongService, string maNhanVien)
        {
            this.thuongService = thuongService;
            this.maNhanVien = maNhanVien;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> thuongs = await thuongService.GetThuongByMaNhanVien(maNhanVien);

            return new ApiResponse()
            {
                Data = thuongs,
                Message = "Lấy thông tin thưởng nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
