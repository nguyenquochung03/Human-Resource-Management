using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TraLuongService;

namespace HumanResourceManagement.Pattern.Command.TraLuong
{
    public class GetTraLuongByMaNhanVienCommand : ICrudCommand
    {
        private readonly ITraLuongService traLuongService;
        private readonly string id;

        public GetTraLuongByMaNhanVienCommand(ITraLuongService traLuongService, string id)
        {
            this.traLuongService = traLuongService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> traLuongs = await traLuongService.GetTraLuongByMaNhanVien(id);

            return new ApiResponse()
            {
                Data = traLuongs,
                Message = "Lấy thông tin trả lương thành công",
                StatusCode = 204
            };
        }
    }
}
