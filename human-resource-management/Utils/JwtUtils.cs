using HumanResourceManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HumanResourceManagement.Utils
{
    public class JwtUtils
    {
        private IConfiguration _config;
        public JwtUtils(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string GenerateToken(TaiKhoan taiKhoan)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"] ?? ""));
            var credentials = new SigningCredentials(secretKeyBytes, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, taiKhoan.Username),
                new Claim(ClaimTypes.Email, taiKhoan.Email),
                new Claim(ClaimTypes.Sid, taiKhoan.Id.ToString()),
            };

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddHours(2),
              signingCredentials: credentials
            );

            return jwtTokenHandler.WriteToken(token);
        }

       
    }
}
