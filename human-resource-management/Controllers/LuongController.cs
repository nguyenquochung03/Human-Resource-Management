using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.Luong;
using HumanResourceManagement.Services.LuongService;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuongController : ControllerBase
    {
        private readonly ILuongService luongService;

        public LuongController(ILuongService luongService)
        {
            this.luongService = luongService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLuongs()
        {
            ICrudCommand command = new GetLuongsCommand(luongService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetLuongsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] DateTime? month = null, [FromQuery] string maNhanVien = "")
        {
            ICrudCommand command = new GetLuongsByPageCommand(luongService, month, page, pageSize, maNhanVien);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLuongById([FromRoute] string id)
        {
            ICrudCommand command = new GetLuongByIdCommand(luongService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("MaNhanVien")]
        public async Task<IActionResult> GetLuongByMaNhanVien([FromQuery] string maNhanVien)
        {
            ICrudCommand command = new GetLuongByMaNhanVienCommand(luongService, maNhanVien);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }



    }
}
