using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter;
using HumanResourceManagement.Response;
using HumanResourceManagement.Services.TrinhDoHocVanService;
using System.Security.Cryptography;

namespace HumanResourceManagement.Pattern.Command.TrinhDoHocVan
{
    public class GetTrinhDoHocVanByIdEmployeeCommand : ICrudCommand
    {
        private readonly ITrinhDoHocVanService _trinhDoHocVanService;
        private readonly string employeeId;

        public GetTrinhDoHocVanByIdEmployeeCommand(ITrinhDoHocVanService trinhDoHocVanService, string employeeId)
        {
            _trinhDoHocVanService = trinhDoHocVanService;
            this.employeeId = employeeId;
        }

        public async Task<ApiResponse> Execute()
        {
            object trinhDoHocVan = await _trinhDoHocVanService.GetTrinhDoHocVanByEmployeeId(employeeId);

            return new ApiResponse()
            {
                Data = trinhDoHocVan,
                Message = "Lấy trình độ học vấn thành công",
                StatusCode = 200
            };
        }
    }
}
