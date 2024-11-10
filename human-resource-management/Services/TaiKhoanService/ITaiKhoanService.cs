using HumanResourceManagement.DTOs;
using HumanResourceManagement.Response;

namespace HumanResourceManagement.Services.TaiKhoanService
{
    public interface ITaiKhoanService
    {
        Task<TokenResponse> DangNhap(TaiKhoanDTO taiKhoanDTO);
        Task<object> DangKi(DangKiDTO taiKhoanDTO);
        Task<object> GetPrincipal(string username);
    }
}
