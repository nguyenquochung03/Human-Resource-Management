using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.HieuSuat;
using HumanResourceManagement.Services.HieuSuatService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HieuSuatController : ControllerBase
    {
        private readonly IHieuSuatService _hieuSuatService;

        public HieuSuatController(IHieuSuatService hieuSuatService)
        {
            _hieuSuatService = hieuSuatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHieuSuats()
        {
            ICrudCommand command = new GetHieuSuatsCommand(_hieuSuatService);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetHieuSuatsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            ICrudCommand command = new GetHieuSuatsByPageCommand(_hieuSuatService, page, pageSize);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("MaNhanVien")]
        public async Task<IActionResult> GetHieuSuatByMaNhanVien([FromQuery] string maNhanVien)
        {
            ICrudCommand command = new GetHieuSuatByMaNhanVienCommand(_hieuSuatService, maNhanVien);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> AddHieuSuat([FromBody] HieuSuatDTO hieuSuatDTO)
        {
            ICrudCommand command = new ThemHieuSuatCommand(_hieuSuatService, hieuSuatDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> SuaHieuSuat([FromRoute] string id, [FromBody] HieuSuatDTO hieuSuatDTO)
        {
            ICrudCommand command = new CapNhatHieuSuatCommand(_hieuSuatService, hieuSuatDTO, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaHieuSuat([FromRoute] string id)
        {
            ICrudCommand command = new XoaHieuSuatCommand(_hieuSuatService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
