using Catalog.Service.EventHandlers.Commands;
using Catalog.Services.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController(IProductQueryService productQueryService,
        IMediator mediator) : ControllerBase
    {
        private readonly IProductQueryService _productQueryService = productQueryService;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, string? ids = null)
        {
            IEnumerable<int>? products = null;
            if (!string.IsNullOrEmpty(ids))
            {
                products = ids.Split(",").Select(x => Convert.ToInt32(x));
            }
            return Ok(await _productQueryService.GetAllAsync(page, take, products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productQueryService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
