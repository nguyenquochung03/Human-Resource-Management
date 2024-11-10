using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.ChamCong;
using HumanResourceManagement.Pattern.Command.LichLam;
using HumanResourceManagement.Services.LichLamService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichLamController : ControllerBase
    {
        private readonly ILichLamService lichLamService;

        public LichLamController(ILichLamService lichLamService)
        {
            this.lichLamService = lichLamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLichLams()
        {
            ICrudCommand command = new GetLichLamsCommand(lichLamService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetLichLamsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            ICrudCommand command = new GetLichLamsByPageCommand(lichLamService, page, pageSize);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("day")]
        public async Task<IActionResult> GetLichLamsFromTo([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            ICrudCommand command = new GetLichLamFromTo(lichLamService, from, to);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("LichLamNhanVien")]
        public async Task<IActionResult> GetLichLamNhanViens()
        {
            ICrudCommand command = new GetLichLamNhanViensCommand(lichLamService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLichLamById([FromRoute] string id)
        {
            ICrudCommand command = new GetLichLamByIdCommand(lichLamService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("nhanvien/{id}")]
        public async Task<IActionResult> GetLichLamsByNvId([FromRoute] string id)
        {
            ICrudCommand command = new GetLichLamByMaNhanVienCommand(lichLamService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> AddLichLam([FromBody] LichLamDTO lichLamDTO)
        {

            ICrudCommand command = new ThemLichLamCommand(lichLamService, lichLamDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> SuaLichLam([FromRoute] string id, [FromBody] LichLamDTO lichLamDTO)
        {
            ICrudCommand command = new CapNhatThongTinLichLamCommand(lichLamService, lichLamDTO, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaLichLamViec([FromRoute] string id)
        {
            ICrudCommand command = new XoaLichLamCommand(lichLamService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddLichLamForNvs([FromBody] CDLichLamForNhanVienDTO dto)
        {
            ICrudCommand command = new ThemLichLamNhanVienCommand(lichLamService, dto);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLichLamForNvs([FromBody] CDLichLamForNhanVienDTO dto)
        {
            ICrudCommand command = new XoaLichLamNhanVienCommand(lichLamService, dto);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
