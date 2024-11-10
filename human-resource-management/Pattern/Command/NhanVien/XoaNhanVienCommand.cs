using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NhanVienService;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public class XoaNhanVienCommand : ICrudCommand
    {
        private readonly INhanVienService _nhanVienService;
        private readonly string _id;

        public XoaNhanVienCommand(INhanVienService nhanVienService, string id)
        {
            _nhanVienService = nhanVienService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _nhanVienService.XoaNhanVien(_id);
            return new ApiResponse()
            {
                Message = "Xóa nhân viên thành công",
                StatusCode = 204
            };
        }
    }
}
