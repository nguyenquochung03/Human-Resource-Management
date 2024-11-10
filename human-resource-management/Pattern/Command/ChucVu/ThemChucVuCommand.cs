using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChucVuService;

namespace HumanResourceManagement.Pattern.Command.ChucVu
{
    public class ThemChucVuCommand : ICrudCommand
    {
        private readonly IChucVuService _chucVuService;
        private readonly ChucVuDTO _chucVuDTO;

        public ThemChucVuCommand(IChucVuService chucVuService, ChucVuDTO chucVuDTO)
        {
            _chucVuService = chucVuService;
            _chucVuDTO = chucVuDTO;
        }

        public async Task<ApiResponse> Execute()
        {
            await _chucVuService.ThemChucVu(_chucVuDTO);
            return new ApiResponse()
            {
                StatusCode = 201,
                Data = "",
                Message = "Thêm chức vụ thành công"
            };
        }
    }
}
