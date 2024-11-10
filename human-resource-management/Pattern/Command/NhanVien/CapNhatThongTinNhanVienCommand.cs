using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NhanVienService;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public class CapNhatThongTinNhanVienCommand : ICrudCommand
    {
        private readonly INhanVienService _nhanVienService;
        private readonly NhanVienDTO _nhanVienDTO;
        private readonly string _id;

        public CapNhatThongTinNhanVienCommand(INhanVienService nhanVienService, NhanVienDTO nhanVienDTO, string id)
        {
            _nhanVienService = nhanVienService;
            _nhanVienDTO = nhanVienDTO;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _nhanVienService.CapNhatNhanVien(_id, _nhanVienDTO);
            return new ApiResponse()
            {
                Message = "Cập nhật thông tin nhân viên thành công",
                StatusCode = 204
            };
        }
    }
}
