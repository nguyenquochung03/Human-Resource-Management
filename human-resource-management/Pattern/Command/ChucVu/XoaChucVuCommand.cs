using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChucVuService;

namespace HumanResourceManagement.Pattern.Command.ChucVu
{
    public class XoaChucVuCommand : ICrudCommand
    {
        private readonly IChucVuService _chucVuService;
        private readonly string _id;

        public XoaChucVuCommand(IChucVuService chucVuService, string id)
        {
            _chucVuService = chucVuService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _chucVuService.XoaChucVu(_id);
            return new ApiResponse()
            {
                Message = "Xóa chức vụ thành công",
                StatusCode = 204
            };
        }
    }
}
