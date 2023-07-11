using ITHealth.Data.Entities;
using ITHealth.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITHealth.Web.API.Infrastructure.Services
{
    public class JWTSecurityHandler
    {
        public static string GenerateLoginToken(JWTSecuritySettings jwtSecuritySettings, User user, string role)
        {
            return GenerateToken(jwtSecuritySettings, user, role, DateTime.UtcNow.AddDays(1));
        }

        public static string GenerateInviteUserToken(JWTSecuritySettings jwtSecuritySettings, User user, string role)
        {
            return GenerateToken(jwtSecuritySettings, user, role, DateTime.UtcNow.AddMonths(1));
        }

        private  static string GenerateToken(JWTSecuritySettings jwtSecuritySettings, User user, string role, DateTime expires)
        {
            if (user == null || role == null)
            {
                return string.Empty;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecuritySettings.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwtSecuritySettings.Issuer,
                jwtSecuritySettings.Audience,
                claims,
                expires: expires,
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
