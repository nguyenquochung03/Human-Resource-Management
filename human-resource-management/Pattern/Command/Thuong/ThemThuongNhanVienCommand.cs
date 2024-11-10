using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ThuongService;

namespace HumanResourceManagement.Pattern.Command.Thuong
{
    public class ThemThuongNhanVienCommand : ICrudCommand
    {
        private readonly IThuongService thuongService;
        private readonly CDKhoanThuongNV cDKhoanThuongNV;

        public ThemThuongNhanVienCommand(IThuongService thuongService, CDKhoanThuongNV cDKhoanThuongNV)
        {
            this.thuongService = thuongService;
            this.cDKhoanThuongNV = cDKhoanThuongNV;
        }

        public async Task<ApiResponse> Execute()
        {
            object thuong = await thuongService.ThemThuongNhanVien(cDKhoanThuongNV);

            return new ApiResponse()
            {
                Data = thuong,
                Message = "Thêm thưởng cho nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
