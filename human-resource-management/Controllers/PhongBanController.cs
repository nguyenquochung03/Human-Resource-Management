using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.PhongBan;
using HumanResourceManagement.Services.PhongBanService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhongBanController : ControllerBase
    {
        private readonly IPhongBanService _phongBanService;

        public PhongBanController(IPhongBanService phongBanService)
        {
            _phongBanService = phongBanService;

        }

        [HttpGet]
        public async Task<IActionResult> GetPhongBans()
        {
            ICrudCommand command = new GetPhongBansCommand(_phongBanService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPhongBansByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            ICrudCommand command = new GetPhongBansByPageCommand(_phongBanService, page, pageSize);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhongBanById([FromRoute] string id)
        {
            ICrudCommand command = new GetPhongBanByIdCommand(_phongBanService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> ThemPhongBan([FromBody] PhongBanDTO phongBanDto)
        {
            ICrudCommand command = new ThemPhongBanCommand(_phongBanService, phongBanDto);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> CapNhatPhongBan([FromRoute] string id, [FromBody] PhongBanDTO phongBanDto)
        {
            ICrudCommand command = new CapNhatPhongBanCommand(_phongBanService, phongBanDto, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaPhongBan([FromRoute] string id)
        {
            ICrudCommand command = new XoaPhongBanCommand(_phongBanService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());

        }
    }
}