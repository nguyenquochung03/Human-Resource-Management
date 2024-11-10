using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChucVuService;

namespace HumanResourceManagement.Pattern.Command.ChucVu
{
    public class GetChucVusCommand : ICrudCommand
    {
        private readonly IChucVuService _chucVuService;

        public GetChucVusCommand(IChucVuService chucVuService)
        {
            _chucVuService = chucVuService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> chucVus = await _chucVuService.GetChucVus();
            return new ApiResponse()
            {
                Data = chucVus,
                StatusCode = 200,
                Message = "Lấy thông tin chức vụ thành công"
            };
        }
    }
}
