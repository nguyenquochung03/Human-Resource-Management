using HumanResourceManagement.Response;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;

namespace HumanResourceManagement.Pattern.Command.PhuCap
{
    public class GetPhuCapNhanVienByEmployeeIdCommand : ICrudCommand
    {
        private readonly MyPhuCapsService myPhuCapsService;
        private readonly string _employeeId;

        public GetPhuCapNhanVienByEmployeeIdCommand(MyPhuCapsService myPhuCapsService, string employeeId)
        {
            this.myPhuCapsService = myPhuCapsService;
            _employeeId = employeeId;
        }

        public async Task<ApiResponse> Execute()
        {
            ICollection<Models.PhuCap> phuCapNhanViens = await myPhuCapsService.GetPhuCapNhanVienByEmployeeId(_employeeId);

            return new ApiResponse()
            {
                Data = phuCapNhanViens,
                Message = "Lấy thông tin phụ cấp nhân viên thành công",
                StatusCode = 200
            };
        }
    }
}
