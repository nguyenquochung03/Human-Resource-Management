using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HieuSuatService;

namespace HumanResourceManagement.Pattern.Command.HieuSuat
{
    public class XoaHieuSuatCommand : ICrudCommand
    {
        private readonly IHieuSuatService hieuSuatService;
        private readonly string id;

        public XoaHieuSuatCommand(IHieuSuatService hieuSuatService, string id)
        {
            this.hieuSuatService = hieuSuatService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await hieuSuatService.XoaHieuSuat(id);

            return new ApiResponse()
            {
                Message = "Xóa hiệu suất thành công",
                StatusCode = 204
            };
        }
    }
}
