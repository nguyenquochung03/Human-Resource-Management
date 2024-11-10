using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.PhuCap;
using HumanResourceManagement.Services.PhuCapService;
using HumanResourceManagement.Services.PhuCapService.PhuCapService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhuCapController : ControllerBase
    {
        private readonly MyPhuCapsService phuCapService;

        public PhuCapController(MyPhuCapsService phuCapService)
        {
            this.phuCapService = phuCapService;

        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> CreatePhuCap([FromBody] PhuCapDTO phuCapDTO)
        {
            ICrudCommand command = new ThemPhuCapCommand(phuCapService, phuCapDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet]
        public async Task<IActionResult> GetPhuCaps()
        {
            ICrudCommand command = new GetPhuCapsCommand(phuCapService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetLuongsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            ICrudCommand command = new GetPhuCapsByPageCommand(phuCapService, page, pageSize);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("PhuCapNhanVien")]
        public async Task<IActionResult> GetPhuCapNhanViens()
        {
            ICrudCommand command = new GetPhuCapNhanViensCommand(phuCapService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhuCapById([FromRoute] string id)
        {
            ICrudCommand command = new GetPhuCapByIDCommand(phuCapService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetPhuCapByEmployeeId([FromQuery] string employeeId)
        {
            ICrudCommand command = new GetPhuCapNhanVienByEmployeeIdCommand(phuCapService, employeeId);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> SuaPhuCap([FromRoute] string id, [FromBody] PhuCapDTO phuCapDTO)
        {
            ICrudCommand command = new SuaPhuCapCommand(phuCapService, phuCapDTO, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaPhuCap([FromRoute] string id)
        {
            ICrudCommand command = new XoaPhuCapCommand(phuCapService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> AddPhuCapForNv([FromBody] CDPhuCapForNhanVienDTO dto)
        {
            ICrudCommand command = new AddPhuCapForNvCommand(phuCapService, dto);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("delete-by-phucapId-employeeId")]
        public async Task<IActionResult> XoaPhuCapNhanVien([FromBody] CDPhuCapForNhanVienDTO dto)
        {
            ICrudCommand command = new XoaPhuCapNhanVienCommand(phuCapService, dto);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
