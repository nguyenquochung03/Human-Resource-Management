using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class TaiKhoanProfile
    {
        public object MapToDto(TaiKhoan taiKhoan)
        {
            return new
            {
                Id = taiKhoan.Id,
                Name = taiKhoan.Name,
                Username = taiKhoan.Username,
                Email = taiKhoan.Email,
            };
        }
    }
}
