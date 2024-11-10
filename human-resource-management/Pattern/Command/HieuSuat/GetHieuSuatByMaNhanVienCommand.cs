using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HieuSuatService;

namespace HumanResourceManagement.Pattern.Command.HieuSuat
{
    public class GetHieuSuatByMaNhanVienCommand : ICrudCommand
    {
        private readonly IHieuSuatService hieuSuatService;
        private readonly string maNhanVien;

        public GetHieuSuatByMaNhanVienCommand(IHieuSuatService hieuSuatService, string maNhanVien)
        {
            this.hieuSuatService = hieuSuatService;
            this.maNhanVien = maNhanVien;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> hieuSuats = await hieuSuatService.GetHieuSuatByMaNhanVien(maNhanVien);

            return new ApiResponse()
            {
                Data = hieuSuats,
                Message = "Lấy thông tin hiệu suất thành công",
                StatusCode = 200
            };
        }
    }
}
