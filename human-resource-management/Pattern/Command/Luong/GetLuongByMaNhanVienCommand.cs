using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LuongService;

namespace HumanResourceManagement.Pattern.Command.Luong
{
    public class GetLuongByMaNhanVienCommand : ICrudCommand
    {
        private readonly ILuongService _luongService;
        private readonly string _maNhanVien;

        public GetLuongByMaNhanVienCommand(ILuongService luongService, string maNhanVien)
        {
            _luongService = luongService;
            _maNhanVien = maNhanVien;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> luongs = await _luongService.GetLuongByMaNhanVien(_maNhanVien);

            return new ApiResponse()
            {
                Data = luongs,
                Message = "Lấy thông tin lương thành công",
                StatusCode = 200
            };
        }
    }
}
