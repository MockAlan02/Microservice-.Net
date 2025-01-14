using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsInStockController(IMediator mediator) :ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPut]
        public async Task<IActionResult> Update(ProductInStockUpdateStockCommand command)
        {
            await _mediator.Publish(command);
            return NoContent();
        }
    }
}
