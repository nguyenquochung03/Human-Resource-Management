using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhongBanService;

namespace HumanResourceManagement.Pattern.Command.PhongBan
{
    public class GetPhongBanByIdCommand : ICrudCommand
    {
        private readonly IPhongBanService _phongBanService;
        private readonly string _id;

        public GetPhongBanByIdCommand(IPhongBanService phongBanService, string id)
        {
            _phongBanService = phongBanService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            Models.PhongBan phongBan = await _phongBanService.GetPhongBanById(_id);
            return new ApiResponse
            {
                Data = phongBan,
                Message = "Lấy thông tin phòng ban thành công",
                StatusCode = 200
            };
        }
    }
}
