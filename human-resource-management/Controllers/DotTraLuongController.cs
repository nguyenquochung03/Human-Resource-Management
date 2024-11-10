using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.DotTraLuong;
using HumanResourceManagement.Services.DotTraLuongService;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotTraLuongController : ControllerBase
    {
        private readonly IDotTraLuongService dotTraLuongService;

        public DotTraLuongController(IDotTraLuongService dotTraLuongService)
        {
            this.dotTraLuongService = dotTraLuongService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDotTraLuongs()
        {
            ICrudCommand command = new GetDotTraLuongsCommand(dotTraLuongService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDotTraLuongsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] DateTime? month = null)
        {
            ICrudCommand command = new GetDotTraLuongsByPage(dotTraLuongService, page, pageSize, month);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDotTraLuongById([FromRoute] string id)
        {
            ICrudCommand command = new GetDotTraLuongByIdCommand(dotTraLuongService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaDotTraLuong([FromRoute] string id)
        {
            ICrudCommand command = new XoaDotTraLuongCommand(dotTraLuongService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
