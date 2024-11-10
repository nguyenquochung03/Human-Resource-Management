using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NghiVangService;

namespace HumanResourceManagement.Pattern.Command.NghiVang
{
    public class GetNghiVangsFromToCommand : ICrudCommand
    {
        private readonly INghiVangService nghiVangService;
        private readonly string id;
        private readonly DateTime month;

        public GetNghiVangsFromToCommand(INghiVangService nghiVangService, string id, DateTime month)
        {
            this.nghiVangService = nghiVangService;
            this.id = id;
            this.month = month;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> nghiVangs = await nghiVangService.GetNghiVangsFromTo(id, month);

            return new ApiResponse()
            {
                Data = nghiVangs,
                Message = "Lấy thông tin nghỉ vắng nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
