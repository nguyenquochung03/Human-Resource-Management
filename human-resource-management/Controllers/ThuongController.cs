using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.Thuong;
using HumanResourceManagement.Services.ThuongService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongController : ControllerBase
    {
        private readonly IThuongService _thuongService;

        public ThuongController(IThuongService thuongService)
        {
            _thuongService = thuongService;
        }

        [HttpGet]
        public async Task<IActionResult> GetThuongs()
        {
            ICrudCommand command = new GetThuongsCommand(_thuongService);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetThuongsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            ICrudCommand command = new GetThuongsByPageCommand(_thuongService, page, pageSize);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("ThuongNhanVien")]
        public async Task<IActionResult> GetThuongNhanVien()
        {
            ICrudCommand command = new GetThuongNhanVienCommand(_thuongService);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetThuongById([FromRoute] string id)
        {
            ICrudCommand command = new GetThuongByIdCommand(_thuongService, id);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpGet("nhanvien/{id}")]
        public async Task<IActionResult> GetLThuongsByNvId([FromRoute] string id)
        {
            ICrudCommand command = new GetThuongByMaNhanVienCommand(_thuongService, id);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());

        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> AddThuong([FromBody] ThuongDTO thuongDTO)
        {
            ICrudCommand command = new ThemThuongCommand(_thuongService, thuongDTO);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> CapNhatKhoanThuong([FromRoute] string id, [FromBody] ThuongDTO thuongDTO)
        {
            ICrudCommand command = new CapNhatThongTinThuongCommand(_thuongService, thuongDTO, id);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaKhoanThuong([FromRoute] string id)
        {
            ICrudCommand command = new XoaThuongCommand(_thuongService, id);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> AddThuongForNVs([FromBody] CDKhoanThuongNV cdKhoanThuongNV)
        {
            ICrudCommand command = new ThemThuongNhanVienCommand(_thuongService, cdKhoanThuongNV);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

       

        [HttpDelete("delete")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> DeleteThuongForNVs([FromBody] CDKhoanThuongNV cdKhoanThuongNV)
        {
            ICrudCommand command = new XoaThuongNhanVienCommand(_thuongService, cdKhoanThuongNV);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
