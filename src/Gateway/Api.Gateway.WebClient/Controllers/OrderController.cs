using Api.Gateway.Domain.Order.Command;
using Api.Gateway.Proxies.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("Api/[controller]s")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController(IOrderProxy orderProxy, ICustomerProxy customerProxy) : ControllerBase
    {
        private readonly IOrderProxy _orderProxy = orderProxy;
        private readonly ICustomerProxy _customerProxy = customerProxy;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page = 1, int take = 10, string ids = "")
        {
            IEnumerable<int>? ordersIds = null;
            if (!string.IsNullOrEmpty(ids))
            {
                ordersIds = ids.Split(",").Select(x => int.Parse(x));
            }
            var result = await _orderProxy.GetAllAsync(page, take, ordersIds!);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _orderProxy.GetAsync(id);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _orderProxy.CreateAsync(command);
            return Ok();

        }
    }
}
