using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChucVuService;

namespace HumanResourceManagement.Pattern.Command.ChucVu
{
    public class GetChucVusByPhongBanIdCommand : ICrudCommand
    {
        private readonly IChucVuService _chucVuService;
        private readonly string _phongBanId;

        public GetChucVusByPhongBanIdCommand(IChucVuService chucVuService, string phongBanId)
        {
            _chucVuService = chucVuService;
            _phongBanId = phongBanId;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> chucVus = await _chucVuService.GetChucVuByPhongBanId(_phongBanId);
            return new ApiResponse()
            {
                Data = chucVus,
                StatusCode = 200,
                Message = "Lấy thông tin chức vụ thành công"
            };
        }
    }
}
