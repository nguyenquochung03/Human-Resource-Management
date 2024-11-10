using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChucVuService;

namespace HumanResourceManagement.Pattern.Command.ChucVu
{
    public class GetChucVuByIdCommand : ICrudCommand
    {
        private readonly IChucVuService _chucVuService;
        private readonly string _id;

        public GetChucVuByIdCommand(IChucVuService chucVuService, string id)
        {
            _chucVuService = chucVuService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            object chucVu = await _chucVuService.GetChucVuById(_id);
            return new ApiResponse()
            {
                Data = chucVu,
                Message = "Lấy thông tin chức vụ thành công",
                StatusCode = 200
            };
        }
    }
}
