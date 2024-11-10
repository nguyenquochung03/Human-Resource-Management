using HumanResourceManagement.Response;
using HumanResourceManagement.Services.LichLamService;

namespace HumanResourceManagement.Pattern.Command.LichLam
{
    public class GetLichLamFromTo : ICrudCommand
    {
        private readonly ILichLamService _service;
        private readonly DateTime from;
        private readonly DateTime to;

        public GetLichLamFromTo(ILichLamService service, DateTime from, DateTime to)
        {
            _service = service;
            this.from = from;
            this.to = to;
        }

        public async Task<ApiResponse> Execute()
        {
            var data = await _service.GetLichLamsFromTo(from, to);
            return new ApiResponse
            {
                Data = data,
                Message = "Success",
                StatusCode = 200
            };
        }
    }
}
