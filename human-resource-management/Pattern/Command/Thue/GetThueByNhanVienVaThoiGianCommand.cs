using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThueService;

namespace HumanResourceManagement.Pattern.Command.Thue
{
    public class GetThueByNhanVienVaThoiGianCommand : ICrudCommand
    {
        private readonly IThueService thueService;
        private string maNhanVien;
        private int thang;
        private int nam;

        public GetThueByNhanVienVaThoiGianCommand(IThueService thueService, string maNhanVien, int thang, int nam)
        {
            this.thueService = thueService;
            this.maNhanVien = maNhanVien;
            this.thang = thang;
            this.nam = nam;
        }
        public async Task<ApiResponse> Execute()
        {
            var thues = await thueService.GetThueByNhanVienVaThoiGian(maNhanVien, thang, nam);

            return new ApiResponse()
            {
                Data = thues,
                Message = "Lấy thông tin thuế thành công",
                StatusCode = 200
            };
        }
    }
}
