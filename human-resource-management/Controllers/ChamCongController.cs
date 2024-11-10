using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.ChamCong;
using HumanResourceManagement.Services.ChamCongService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamCongController : ControllerBase
    {
        private readonly IChamCongService _chamCongService;

        public ChamCongController(IChamCongService chamCongService)
        {
            _chamCongService = chamCongService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChamCongs()
        {
            ICrudCommand command = new GetChamCongsCommand(_chamCongService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetChamCongsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] DateTime? day = null)
        {
            ICrudCommand command = new GetChamCongsByPageCommand(_chamCongService, page, pageSize, day);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("day")]
        public async Task<IActionResult> GetChamCongsByDays([FromQuery] DateTime day)
        {
            ICrudCommand command = new GetChamCongsByDayCommand(_chamCongService, day);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("day/{maNhanVien}")]
        public async Task<IActionResult> GetChamCongsFromTo([FromRoute] string maNhanVien, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            ICrudCommand command = new GetChamCongsFromToCommand(_chamCongService, maNhanVien, from, to);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("MaNhanVien")]
        public async Task<IActionResult> GetChamCongByMaNhanVien([FromQuery] string id)
        {
            ICrudCommand command = new GetChamCongByMaNhanVienCommand(_chamCongService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [ServiceFilter(typeof(CustomValidationFilter))]
        [HttpPost]
        public async Task<IActionResult> AddChamCong([FromBody] ChamCongDTO chamCongDTO)
        {
            ICrudCommand command = new ThemChamCongNhanVienCommand(_chamCongService, chamCongDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [ServiceFilter(typeof(CustomValidationFilter))]
        [HttpPut("{id}")]
        public async Task<IActionResult> SuaChamCong([FromRoute] string id, [FromBody] ChamCongDTO chamCongDTO)
        {
            ICrudCommand command = new SuaChamCongCommand(_chamCongService, chamCongDTO, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaChamCong([FromRoute] string id)
        {
            ICrudCommand command = new XoaChamCongCommand(_chamCongService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
