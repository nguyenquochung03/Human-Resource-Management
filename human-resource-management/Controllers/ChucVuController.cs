using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.ChucVu;
using HumanResourceManagement.Services.ChucVuService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuService _chucVuService;

        public ChucVuController(IChucVuService chucVuService)
        {
            _chucVuService = chucVuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChucVus()
        {
            ICrudCommand command = new GetChucVusCommand(_chucVuService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetChucVusByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string phongBanId = "", [FromQuery] string searchChucVu = "")
        {
            ICrudCommand command = new GetChucVusByPageCommand(_chucVuService, page, pageSize, phongBanId, searchChucVu);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChucVu>> GetChucVuById([FromRoute] string id)
        {
            ICrudCommand command = new GetChucVuByIdCommand(_chucVuService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("phongBan/{id}")]
        public async Task<ActionResult<ChucVu>> GetChucVusByPhongBanId([FromRoute] string id)
        {
            ICrudCommand command = new GetChucVusByPhongBanIdCommand(_chucVuService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> ThemChucVu([FromBody] ChucVuDTO chucVuDto)
        {
            ICrudCommand command = new ThemChucVuCommand(_chucVuService, chucVuDto);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> CapNhatChucVu([FromRoute] string id, [FromBody] ChucVuDTO chucVuDto)
        {
            ICrudCommand command = new SuaChucVuCommand(_chucVuService, chucVuDto, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaChucVu([FromRoute] string id)
        {
            ICrudCommand command = new XoaChucVuCommand(_chucVuService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}