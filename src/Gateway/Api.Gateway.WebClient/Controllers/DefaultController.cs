using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "running...";
        }
    }
}
