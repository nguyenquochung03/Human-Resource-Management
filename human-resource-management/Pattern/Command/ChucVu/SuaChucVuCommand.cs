using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.ChucVuService;

namespace HumanResourceManagement.Pattern.Command.ChucVu
{
    public class SuaChucVuCommand : ICrudCommand
    {
        private readonly IChucVuService _chucVuService;
        private readonly ChucVuDTO chucVuDTO;
        private readonly string id;

        public SuaChucVuCommand(IChucVuService chucVuService, ChucVuDTO chucVuDTO, string id)
        {
            _chucVuService = chucVuService;
            this.chucVuDTO = chucVuDTO;
            this.id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            await _chucVuService.SuaChucVu(id, chucVuDTO);
            return new ApiResponse()
            {
                Message = "Cập nhật chức vụ thành công",
                StatusCode = 204,
            };
        }
    }
}
