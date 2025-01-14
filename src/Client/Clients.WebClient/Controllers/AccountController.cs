using Microsoft.AspNetCore.Mvc;
using Clients.WebClient.Helper;
using Microsoft.Extensions.Options;
using Clients.WebClient.Domain.Settings;
namespace Clients.WebClient.Controllers
{

    public class AccountController(IHttpContextAccessor httpContext, IOptions<JwtSetting> options) : Controller
    {
        private readonly JwtSetting _jwt = options.Value;

        [HttpGet]
        public async Task<IActionResult> Connect(string token)
        {
            try
            {
                await TokenHelper.ValidateToken(token, httpContext, _jwt);
                return Redirect("/");
            }
            catch (Exception ex)
            {
                return BadRequest("Token invalid");
            }

        }
    }
}
