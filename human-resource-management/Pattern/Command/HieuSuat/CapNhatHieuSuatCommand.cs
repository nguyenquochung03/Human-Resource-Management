using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HieuSuatService;

namespace HumanResourceManagement.Pattern.Command.HieuSuat
{
    public class CapNhatHieuSuatCommand : ICrudCommand
    {
        private readonly IHieuSuatService hieuSuatService;
        private readonly HieuSuatDTO hieuSuatDTO;
        private readonly string id;

        public CapNhatHieuSuatCommand(IHieuSuatService hieuSuatService, HieuSuatDTO hieuSuatDTO, string id)
        {
            this.hieuSuatService = hieuSuatService;
            this.hieuSuatDTO = hieuSuatDTO;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await hieuSuatService.CapNhatHieuSuat(id, hieuSuatDTO);

            return new ApiResponse()
            {
                Message = "Cập nhật hiệu suất thành công",
                StatusCode = 204
            };
        }
    }
}
