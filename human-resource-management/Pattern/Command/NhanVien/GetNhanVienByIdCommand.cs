using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NhanVienService;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public class GetNhanVienByIdCommand : ICrudCommand
    {
        private readonly INhanVienService _nhanVienService;
        private readonly string _id;

        public GetNhanVienByIdCommand(INhanVienService nhanVienService, string id)
        {
            _nhanVienService = nhanVienService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            object nhanVien = await _nhanVienService.GetNhanVienById(_id);
            return new ApiResponse()
            {
                Data = nhanVien,
                Message = "Lấy nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
