using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HopDongService;

namespace HumanResourceManagement.Pattern.Command.HopDong
{
    public class GetHopDongByEmployeeIdCommand : ICrudCommand
    {
        private readonly IHopDongService _hopDongService;
        private string _employeeId;

        public GetHopDongByEmployeeIdCommand(IHopDongService hopDongService, string employeeId)
        {
            _hopDongService = hopDongService;
            _employeeId = employeeId;
        }

        public async Task<ApiResponse> Execute()
        {
            object hopDong = await _hopDongService.GetHopDongByEmployeeId(_employeeId);

            return new ApiResponse()
            {
                Data = hopDong,
                Message = "Lấy thông tin hợp đồng thành công",
                StatusCode = 200
            };
        }
    }
}
