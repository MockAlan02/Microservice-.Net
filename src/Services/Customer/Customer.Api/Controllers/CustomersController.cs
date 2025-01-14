using Customer.Services.EventHandler.Command;
using Customer.Services.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers
{
    [ApiController]
    [Route("Api/Customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController(ICustomerQueryService customerQuery, IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;
        private readonly ICustomerQueryService _customerQuery = customerQuery;

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string? ids = "")
        {
            IEnumerable<int>? clientsId = null;
            if (!string.IsNullOrEmpty(ids))
            {
                clientsId = ids.Split(",").Select(x => int.Parse(x.ToString()));
            }
            return Ok(await _customerQuery.GetAllAsync(page, take, clientsId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _customerQuery.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
