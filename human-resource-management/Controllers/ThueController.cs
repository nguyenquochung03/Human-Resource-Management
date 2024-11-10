using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.Thue;
using HumanResourceManagement.Services.ThueService;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThueController : ControllerBase
    {
        private readonly IThueService thueService;

        public ThueController(IThueService thueService)
        {
            this.thueService = thueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetThues()
        {
            ICrudCommand command = new GetThueCommand(thueService);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetThuesByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string employeeId = "", [FromQuery] DateTime? filterTime = null)
        {
            ICrudCommand command = new GetThuesByPageCommand(thueService, page, pageSize, employeeId, filterTime);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("{maNhanVien}/thoigian")]
        public async Task<IActionResult> GetThueByNhanVienVaThoiGian([FromRoute] string maNhanVien, [FromQuery] int thang, [FromQuery] int nam)
        {
            ICrudCommand command = new GetThueByNhanVienVaThoiGianCommand(thueService, maNhanVien, thang, nam);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetThueById([FromRoute] string id)
        {
            ICrudCommand command = new GetThueByIdCommand(thueService, id);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("MaNhanVien")]
        public async Task<IActionResult> GetThueByMaNhanVien([FromQuery] string id)
        {
            ICrudCommand command = new GetThueByMaNhanVienCommand(thueService, id);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }


    }
}
