using HumanResourceManagement.Response;
using HumanResourceManagement.Services.HopDongService;

namespace HumanResourceManagement.Pattern.Command.HopDong
{
    public class GetHopDongByIdCommand : ICrudCommand
    {
        private readonly IHopDongService _hopDongService;
        private readonly string _id;

        public GetHopDongByIdCommand(IHopDongService hopDongService, string id)
        {
            _hopDongService = hopDongService;
            _id = id;
        }

        public async Task<ApiResponse> Execute()
        {
            object data = await _hopDongService.GetContractById(_id);

            return new ApiResponse()
            {
                Message = "Lấy hợp đồng thành công",
                StatusCode = 200,
                Data = data
            };
        }
    }
}
