using Library_management_system.Enums;
using Library_management_system.Models.Database.Tables;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Library_management_system.Services
{
    public class JwtServices
    {
        public readonly IConfiguration _configuration;
        public JwtServices(IConfiguration configuration) {
            _configuration = configuration;
                  }

        public string GenerateToken(Users user) {
            var claims = new List<Claim> {
            new Claim(AppClaimTypes.USER_ID,user.Id.ToString()),
            new Claim(AppClaimTypes.USERNAME,user.Username),
            new Claim(AppClaimTypes.USER_TYPE,user.UserType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryMins = double.Parse(_configuration["Jwt:ExpiryMins"]!);
            var token = new JwtSecurityToken(
         issuer: _configuration["Jwt:Issuer"],
         audience: _configuration["Jwt:Issuer"],
         claims: claims,
         expires: DateTime.UtcNow.AddMinutes(expiryMins),
         signingCredentials: creds
     );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
