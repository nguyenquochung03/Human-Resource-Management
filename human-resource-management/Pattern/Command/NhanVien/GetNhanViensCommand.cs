using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NhanVienService;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public class GetNhanViensCommand : ICrudCommand
    {
        private readonly INhanVienService _nhanVienService;

        public GetNhanViensCommand(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> nhanViens = await _nhanVienService.GetNhanViens();

            return new ApiResponse()
            {
                Data = nhanViens,
                Message = "Lấy nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
