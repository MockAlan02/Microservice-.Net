using Identity.Service.EventHandler.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        public async Task<IActionResult> Create(UserCreateCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Data!.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("Authentication")]
        public async Task<IActionResult> Login(UserLoginCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Data!.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
