using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.TraLuong;
using HumanResourceManagement.Services.TraLuongService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraLuongController : ControllerBase
    {
        private readonly ITraLuongService traLuongService;

        public TraLuongController(ITraLuongService traLuongService)
        {
            this.traLuongService = traLuongService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTraLuongs()
        {
            ICrudCommand command = new GetTraLuongsCommand(traLuongService);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetTraLuongsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string employeeName = "", [FromQuery] DateTime? month = null)
        {
            ICrudCommand command = new GetTraLuongsByPageCommand(traLuongService, page, pageSize, employeeName, month);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("MaNhanVien")]
        public async Task<IActionResult> GetTraLuongByMaNhanVien([FromQuery] string id)
        {
            ICrudCommand command = new GetTraLuongByMaNhanVienCommand(traLuongService, id);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("{dotTraLuongId}/{maNhanVien}")]
        public async Task<IActionResult> GetTraLuongById([FromRoute] string dotTraLuongId, [FromRoute] string maNhanVien)
        {
            ICrudCommand command = new GetTraLuongByIdCommand(traLuongService, dotTraLuongId, maNhanVien);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> AddTraLuong([FromBody] TraLuongDTO traLuongDTO)
        {
            ICrudCommand command = new ThemTraLuongCommand(traLuongService, traLuongDTO);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

     
    }
}
