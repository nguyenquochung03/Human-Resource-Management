using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class GetThuongNhanVienCommand : ICrudCommand
    {
        private readonly IThuongService _thuongService;

        public GetThuongNhanVienCommand(IThuongService thuongService)
        {
            _thuongService = thuongService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<Models.ThuongNhanVien> thuongNhanViens = await _thuongService.GetThuongNhanVien();

            return new ApiResponse()
            {
                Data = thuongNhanViens,
                Message = "Lấy thưởng nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
