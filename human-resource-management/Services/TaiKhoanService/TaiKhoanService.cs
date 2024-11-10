using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;
using HumanResourceManagement.Models;
using HumanResourceManagement.Profile;
using HumanResourceManagement.Response;
using HumanResourceManagement.Utils;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Services.TaiKhoanService
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly HumanResourceManagementDbContext humanResourceManagementDbContext;
        private readonly PasswordEncoder passwordEncoder;
        private readonly TaiKhoanProfile taiKhoanProfile;
        private readonly JwtUtils jwtUtils;

        public TaiKhoanService(HumanResourceManagementDbContext humanResourceManagementDbContext, JwtUtils jwtUtils, PasswordEncoder passwordEncoder, TaiKhoanProfile taiKhoanProfile)
        {
            this.humanResourceManagementDbContext = humanResourceManagementDbContext;
            this.jwtUtils = jwtUtils;
            this.passwordEncoder = passwordEncoder;
            this.taiKhoanProfile = taiKhoanProfile;
        }
        public async Task<object> DangKi(DangKiDTO taiKhoanDTO)
        {
            TaiKhoan? checkTaiKhoan = await humanResourceManagementDbContext.TaiKhoans
                .SingleOrDefaultAsync(
                    tk => tk.Username == taiKhoanDTO.Username);

            if (checkTaiKhoan != null)
                throw new ConflictException("Username đã tồn tại");

            if (checkTaiKhoan != null && checkTaiKhoan.Email.Equals(taiKhoanDTO.Email))
                throw new ConflictException("Email đã tồn tại");

            TaiKhoan newTaiKhoan = new TaiKhoan();
            newTaiKhoan.Name = taiKhoanDTO.Name;
            newTaiKhoan.Username = taiKhoanDTO.Username;
            newTaiKhoan.Email = taiKhoanDTO.Email;
            newTaiKhoan.Password = passwordEncoder.Encode(taiKhoanDTO.Password);

            await humanResourceManagementDbContext.AddAsync(newTaiKhoan);
            await humanResourceManagementDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<TokenResponse> DangNhap(TaiKhoanDTO taiKhoanDTO)
        {
            TaiKhoan? checkTaiKhoan = await humanResourceManagementDbContext.TaiKhoans
                .SingleOrDefaultAsync(
                    tk => tk.Username == taiKhoanDTO.Username
                    && tk.Password.Equals(passwordEncoder.Encode(taiKhoanDTO.Password))
                 )
                ?? throw new ConflictException("Thông tin đăng nhập không chính xác");

            string accessToken = jwtUtils.GenerateToken(checkTaiKhoan);

            TokenResponse tokenResponse = new TokenResponse();
            tokenResponse.AccessToken = accessToken;
            tokenResponse.User = taiKhoanProfile.MapToDto(checkTaiKhoan);
            return tokenResponse;
        }

        public async Task<object> GetPrincipal(string username)
        {
            TaiKhoan? checkTaiKhoan = await humanResourceManagementDbContext.TaiKhoans
                .SingleOrDefaultAsync(
                    tk => tk.Username == username)
                ?? throw new UnauthorizedAccessException("Bạn không có quyền truy cập");

            return taiKhoanProfile.MapToDto(checkTaiKhoan);
        }
    }
}
