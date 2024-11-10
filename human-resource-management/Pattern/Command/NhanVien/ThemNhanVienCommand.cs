using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NhanVienService;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public class ThemNhanVienCommand : ICrudCommand
    {
        private readonly INhanVienService nhanVienService;
        private readonly NhanVienDTO nhanVienDTO;

        public ThemNhanVienCommand(INhanVienService nhanVienService, NhanVienDTO nhanVienDTO)
        {
            this.nhanVienService = nhanVienService;
            this.nhanVienDTO = nhanVienDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object nhanVien = await this.nhanVienService.ThemNhanVien(nhanVienDTO);

            return new ApiResponse()
            {
                Data = nhanVien,
                Message = "Thêm nhân viên thành công",
                StatusCode = 201
            };
        }
    }
}
