using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Services.EventHandler.Command;
using Order.Services.Queries.Interfaces;

namespace Order.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("Api/[Controller]s/")]
    public class OrderController(IMediator mediator,
        IOrderQueryService orderQueryService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IOrderQueryService _queryService = orderQueryService;


        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string ids = "")
        {
            IEnumerable<int>? orders = null;
            if (!string.IsNullOrWhiteSpace(ids))
            {
                orders = ids.Split(",").Select(x => int.Parse(x));
            }
            return Ok(await _queryService.GetAllAsync(page, take, orders));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _queryService.GetAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }

    }
}
