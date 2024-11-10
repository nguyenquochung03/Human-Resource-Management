using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.TrinhDoHocVan;
using HumanResourceManagement.Services.TrinhDoHocVanService;
using HumanResourceManagement.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrinhDoHocVanController : ControllerBase
    {
        private readonly ITrinhDoHocVanService _trinhDoHocVanService;

        public TrinhDoHocVanController(ITrinhDoHocVanService trinhDoHocVanService)
        {
            _trinhDoHocVanService = trinhDoHocVanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrinhDoHocVan()
        {
            ICrudCommand command = new GetTrinhDoHocVansCommand(_trinhDoHocVanService);
             CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> GetTrinhDoHocVansByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            ICrudCommand command = new GetTrinhDoHocVansByPageCommand(_trinhDoHocVanService, page, pageSize);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetTrinhDoHocVanByEmployeeId([FromRoute] string employeeId)
        {
            ICrudCommand command = new GetTrinhDoHocVanByIdEmployeeCommand(_trinhDoHocVanService, employeeId);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> ThemTrinhDoHocVan([FromBody] TrinhDoHocVanDTO trinhDoHocVanDTO)
        {
            ITrinhDoHocVanTargetDTO adapter = new TrinhDoHocVanAdapter(_trinhDoHocVanService);
            ICrudCommand command = new ThemTrinhDoHocVanCommand(adapter, trinhDoHocVanDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public async Task<IActionResult> CapNhatTrinhDoHocVan([FromRoute] string id, [FromBody] TrinhDoHocVanDTO trinhDoHocVanDTO)
        {
            ITrinhDoHocVanTargetDTO adapter = new TrinhDoHocVanAdapter(_trinhDoHocVanService);
            ICrudCommand command = new CapNhatTrinhDoHocVanCommand(adapter, trinhDoHocVanDTO, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaTrinhDoHocVan([FromRoute] string id)
        {
            ICrudCommand command = new XoaTrinhDoHocVanCommand(_trinhDoHocVanService, id);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
