using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NghiVangService;

namespace HumanResourceManagement.Pattern.Command.NghiVang
{
    public class GetNghiVangsCommand : ICrudCommand
    {
        private readonly INghiVangService _nghiVangService;

        public GetNghiVangsCommand(INghiVangService nghiVangService)
        {
            _nghiVangService = nghiVangService;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> nghiVangs = await _nghiVangService.GetNghiVangs();

            return new ApiResponse()
            {
                Data = nghiVangs,
                Message = "Lấy thông tin nghỉ vắng thành công",
                StatusCode = 200
            };
        }
    }
}
