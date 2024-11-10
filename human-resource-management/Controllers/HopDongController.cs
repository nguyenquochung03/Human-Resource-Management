using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.HopDong;
using HumanResourceManagement.Services.HopDongService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HopDongController : ControllerBase
    {
        private readonly IHopDongService _hopDongService;

        public HopDongController(IHopDongService hopDongService)
        {
            _hopDongService = hopDongService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHopDongs()
        {
            ICrudCommand command = new GetHopDongsCommand(_hopDongService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHopDongById([FromRoute] string id)
        {
            ICrudCommand command = new GetHopDongByIdCommand(_hopDongService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetHopDongsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            ICrudCommand command = new GetHopDongsByPageCommand(_hopDongService, page, pageSize);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("employeeId")]
        public async Task<IActionResult> GetHopDongByEmployeeId([FromQuery] string employeeId)
        {
            ICrudCommand command = new GetHopDongByEmployeeIdCommand(_hopDongService, employeeId);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> AddHopDong([FromBody] HopDongDTO hopDongDTO)
        {
            ICrudCommand command = new ThemHopDongCommand(_hopDongService, hopDongDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> SuaHopDong([FromRoute] string id, [FromBody] HopDongDTO hopDongDTO)
        {
            ICrudCommand command = new CapNhatThongTinHopDongCommand(_hopDongService, hopDongDTO, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaHopDong([FromRoute] string id)
        {
            ICrudCommand command = new XoaHopDongCommand(_hopDongService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
