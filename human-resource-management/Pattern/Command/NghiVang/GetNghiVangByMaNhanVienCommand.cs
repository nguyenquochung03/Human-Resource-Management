using HumanResourceManagement.Response;
using HumanResourceManagement.Services.NghiVangService;

namespace HumanResourceManagement.Pattern.Command.NghiVang
{
    public class GetNghiVangByMaNhanVienCommand : ICrudCommand
    {
        private readonly INghiVangService nghiVangService;
        private readonly string id;

        public GetNghiVangByMaNhanVienCommand(INghiVangService nghiVangService, string id)
        {
            this.nghiVangService = nghiVangService;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<object> nghiVangs = await nghiVangService.GetNghiVangByMaNhanVien(id);

            return new ApiResponse()
            {
                Data = nghiVangs,
                Message = "Lấy thông tin nghỉ vắng nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
