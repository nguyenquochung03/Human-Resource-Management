using HumanResourceManagement.DTOs;
using HumanResourceManagement.Pattern.Command;
using HumanResourceManagement.Pattern.Command.TaiKhoan;
using HumanResourceManagement.Services.TaiKhoanService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HumanResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService taiKhoanService;

        public TaiKhoanController(ITaiKhoanService taiKhoanService)
        {
            this.taiKhoanService = taiKhoanService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPrincipal()
        {
            string username = User.Claims.First(u => u.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "";
            ICrudCommand command = new GetPrincipalCommand(taiKhoanService, username);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }

        [HttpPost("login")]
        public async Task<IActionResult> DangNhap([FromBody] TaiKhoanDTO taiKhoanDTO)
        {
            ICrudCommand command = new DangNhapCommand(taiKhoanService, taiKhoanDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd()); 
        }

        [HttpPost("register")]
        public async Task<IActionResult> DangKi([FromBody] DangKiDTO taiKhoanDTO)
        {
            ICrudCommand command = new DangKiCommand(taiKhoanService, taiKhoanDTO);
            CrudControl.getInstance().setCommand(command);
            return Ok(await CrudControl.getInstance().ExecuteCmd());
        }
    }
}
