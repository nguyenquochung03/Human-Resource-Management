using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TraLuongService;

namespace HumanResourceManagement.Pattern.Command.TraLuong
{
    public class GetTraLuongByIdCommand : ICrudCommand
    {
        private readonly ITraLuongService traLuongService;
        private readonly string dotTraLuongId;
        private readonly string maNhanVien;

        public GetTraLuongByIdCommand(ITraLuongService traLuongService, string dotTraLuongId, string maNhanVien)
        {
            this.traLuongService = traLuongService;
            this.dotTraLuongId = dotTraLuongId;
            this.maNhanVien = maNhanVien;
        }

        public async Task<ApiResponse> Execute()
        {
            object traLuong = await traLuongService.GetTraLuongById(dotTraLuongId, maNhanVien);

            return new ApiResponse()
            {
                Data = traLuong,
                Message = "Lấy thông tin trả lương thành công",
                StatusCode = 200
            };
        }
    }
}
