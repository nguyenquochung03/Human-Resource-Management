using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NghiVangService;

namespace HumanResourceManagement.Pattern.Command.NghiVang
{
    public class XoaNghiVangCommand : ICrudCommand
    {
        private readonly INghiVangService nghiVangService;
        private readonly string id;

        public XoaNghiVangCommand(INghiVangService nghiVangService, string id)
        {
            this.nghiVangService = nghiVangService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await nghiVangService.XoaNghiVang(id);

            return new ApiResponse()
            {
                Message = "Xóa nghỉ vắng nhân viên thành công",
                StatusCode = 204
            };
        }
    }
}
