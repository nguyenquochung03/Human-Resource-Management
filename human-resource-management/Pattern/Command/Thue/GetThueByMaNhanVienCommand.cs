using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThueService;

namespace HumanResourceManagement.Pattern.Command.Thue
{
    public class GetThueByMaNhanVienCommand : ICrudCommand
    {
        private readonly IThueService thueService;
        private readonly string maNhanVien;

        public GetThueByMaNhanVienCommand(IThueService thueService, string maNhanVien)
        {
            this.thueService = thueService;
            this.maNhanVien = maNhanVien;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> thues = await thueService.GetThueByMaNhanVien(this.maNhanVien);

            return new ApiResponse()
            {
                Data = thues,
                Message = "Lấy thông tin thuế thành công",
                StatusCode = 200,
            };
        }
    }
}
