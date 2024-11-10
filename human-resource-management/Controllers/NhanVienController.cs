using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.NhanVien;
using HumanResourceManagement.Services.NhanVienService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;

        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNhanViens()
        {
            ICrudCommand command = new GetNhanViensCommand(_nhanVienService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetNhanViensByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string queryType = "", [FromQuery] string queryValue = "")
        {
            ICrudCommand command = new GetNhaViensByPageCommand(_nhanVienService, queryType, queryValue, page, pageSize);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetNhanVienById([FromQuery] string id)
        {
            ICrudCommand command = new GetNhanVienByIdCommand(_nhanVienService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetNhanVienByName([FromQuery] string name)
        {
            ICrudCommand command = new GetNhanVienByNameCommand(_nhanVienService, name);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("get-by-phone")]
        public async Task<IActionResult> GetNhanVienByPhone([FromQuery] string phone)
        {
            ICrudCommand command = new GetNhanVienByPhoneCommand(_nhanVienService, phone);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> ThemNhanVien([FromBody] NhanVienDTO nhanVienDto)
        {
            ICrudCommand command = new ThemNhanVienCommand(_nhanVienService, nhanVienDto);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> CapNhatNhanVien([FromRoute] string id, [FromBody] NhanVienDTO nhanVienDto)
        {
            ICrudCommand command = new CapNhatThongTinNhanVienCommand(_nhanVienService, nhanVienDto, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaNhanVien([FromRoute] string id)
        {
            ICrudCommand command = new XoaNhanVienCommand(_nhanVienService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}