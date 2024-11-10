using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.NghiVang;
using HumanResourceManagement.Services.NghiVangService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NghiVangController : ControllerBase
    {
        private readonly INghiVangService _nghiVangService;

        public NghiVangController(INghiVangService nghiVangService)
        {
            _nghiVangService = nghiVangService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNghiVangs()
        {
            ICrudCommand command = new GetNghiVangsCommand(_nghiVangService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetNghiVangsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            ICrudCommand command = new GetNghiVangsByPageCommand(_nghiVangService, page, pageSize);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("MaNhanVien")]
        public async Task<IActionResult> GetNghiVangByMaNhanVien([FromQuery] string id)
        {
            ICrudCommand command = new GetNghiVangByMaNhanVienCommand(_nghiVangService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());

        }

        [HttpGet("{id}/month")]
        public async Task<IActionResult> GetNghiVangsByMonth([FromRoute] string id, [FromQuery] DateTime month)
        {
            ICrudCommand command = new GetNghiVangsFromToCommand(_nghiVangService, id, month);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
             
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> AddNghiVang([FromBody] NghiVangDTO nghiVangDTO)
        {
            ICrudCommand command = new ThemNghiVangCommand(_nghiVangService, nghiVangDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> SuaNghiVang([FromRoute] string id, [FromBody] NghiVangDTO nghiVangDTO)
        {
            ICrudCommand command = new CapNhatNghiVangCommand(_nghiVangService, nghiVangDTO, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaNghiVang([FromRoute] string id)
        {
            ICrudCommand command = new XoaNghiVangCommand(_nghiVangService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
