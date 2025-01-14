using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers
{
    [Route("/")]
    public class DefaultController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Running...";
        }
    }
}
