using Identity.Domain;
using Identity.Service.EventHandler.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Service.EventHandler.Helper
{

    public static class TokenHelper
    {
        public static async Task GenerateToken(IConfiguration configuration, UserManager<ApplicationUser> userManager, ApplicationUser user, IdentityAccess access)
        {
            var secretKey = configuration.GetValue<string>("SecretKey");
            var key = Encoding.UTF8.GetBytes(secretKey!);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Surname, user.LastName)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(
                    new(ClaimTypes.Role, role)
                    );
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "your_issuer",
                Audience = "your_audience",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            access.AccessToken = tokenHandler.WriteToken(createdToken);
        }
    }
}
