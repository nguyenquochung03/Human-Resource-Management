using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class XoaThuongNhanVienCommand : ICrudCommand
    {
        private readonly IThuongService thuongService;
        private readonly CDKhoanThuongNV cDKhoanThuongNV;

        public XoaThuongNhanVienCommand(IThuongService thuongService, CDKhoanThuongNV cDKhoanThuongNV)
        {
            this.thuongService = thuongService;
            this.cDKhoanThuongNV = cDKhoanThuongNV;
        }

        public async Task<ApiResponse> Execute()
        {
            await thuongService.XoaThuongNhanVien(cDKhoanThuongNV);

            return new ApiResponse()
            {
                Message = "Xóa thưởng nhân viên thành công",
                StatusCode = 204
            };
        }
    }
}
