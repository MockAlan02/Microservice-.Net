using Api.Gateway.Domain.Catalog.Commands;
using Api.Gateway.Proxies.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Api.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("Api/[controller]s")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController(ICatalogProxy catalogProxy) : ControllerBase
    {
        private readonly ICatalogProxy _catalogProxy = catalogProxy;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page = 1, int take = 10, string ids = "")
        {
            IEnumerable<int>? productsIds = null;
            if (!string.IsNullOrEmpty(ids))
            {
                productsIds = ids.Split(",").Select(x => int.Parse(x));
            }
            return Ok(await _catalogProxy.GetAllAsync(page, take, productsIds!));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _catalogProxy.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            await _catalogProxy.CreateAsync(command);
            return Ok();
        }
    }
}
