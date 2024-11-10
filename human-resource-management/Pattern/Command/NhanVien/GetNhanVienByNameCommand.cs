using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NhanVienService;

namespace HumanResourceManagement.Pattern.Command.NhanVien
{
    public class GetNhanVienByNameCommand : ICrudCommand
    {
        private readonly INhanVienService _nhanVienService;
        private readonly string _name;

        public GetNhanVienByNameCommand(INhanVienService nhanVienService, string name)
        {
            _nhanVienService = nhanVienService;
            _name = name;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> nhanViens = await _nhanVienService.GetNhanVienByName(_name);

            return new ApiResponse()
            {
                Data = nhanViens,
                Message = "Lấy nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
