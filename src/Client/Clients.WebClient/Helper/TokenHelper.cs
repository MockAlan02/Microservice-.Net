using Clients.WebClient.Domain.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clients.WebClient.Helper
{
    public static class TokenHelper
    {
        public static async Task ValidateToken(string token, IHttpContextAccessor httpContextAccessor, JwtSetting jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwt.Key); // La misma clave secreta usada al generar el token

          
                var claims = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);


            // Crear una nueva lista de Claims que incluye los Claims validados
            var claimList = claims.Claims.ToList();

            // Agregar un nuevo Claim adicional
            claimList.Add(new Claim("access_token", token));

            // Crear una nueva ClaimsIdentity con los Claims existentes más el nuevo Claim
            var identity = new ClaimsIdentity(claimList, "Bearer");

            // Crear un ClaimsPrincipal con la ClaimsIdentity
            var principal = new ClaimsPrincipal(identity);

            
                httpContextAccessor!.HttpContext!.User = principal;


            // Crear una cookie de autenticación
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false, // Mantener la sesión activa
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(20) // Tiempo de expiración
            };

            // Registrar al usuario
           await httpContextAccessor!.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }
        }
    }

