using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HieuSuatService;

namespace HumanResourceManagement.Pattern.Command.HieuSuat
{
    public class ThemHieuSuatCommand : ICrudCommand
    {
        private readonly IHieuSuatService hieuSuatService;
        private readonly HieuSuatDTO hieuSuatDTO;

        public ThemHieuSuatCommand(IHieuSuatService hieuSuatService, HieuSuatDTO hieuSuatDTO)
        {
            this.hieuSuatService = hieuSuatService;
            this.hieuSuatDTO = hieuSuatDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            object hieuSuat = await hieuSuatService.ThemHieuSuat(hieuSuatDTO);

            return new ApiResponse()
            {
                Data = hieuSuat,
                Message = "Thêm hiệu suất thành công",
                StatusCode = 201
            };
        }
    }
}
