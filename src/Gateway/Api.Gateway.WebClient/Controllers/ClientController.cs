using Api.Gateway.Domain.Customer.Command;
using Api.Gateway.Domain.Identity.Commands;
using Api.Gateway.Proxies.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("Api/[controller]s")]
    
    public class ClientController(ICustomerProxy customerProxy) : ControllerBase
    {
        private readonly ICustomerProxy _customerProxy = customerProxy;


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllAsync(int page = 1, int take = 10, string ids = "")
        {
            IEnumerable<int>? customerIds = null;
            if (!string.IsNullOrEmpty(ids))
            {
                customerIds = ids.Split(",").Select(x => int.Parse(x));
            }

            return Ok(await _customerProxy.GetAllAsync(page, take, customerIds!));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _customerProxy.GetAsync(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateAsync(CustomerCreateCommand command)
        {
            await _customerProxy.CreateAsync(command);
            return Ok();
        }
    }
}
