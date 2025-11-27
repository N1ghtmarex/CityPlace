using Api.StartupConfigurations;
using Application.Abstractions.Models;
using Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpPost("registration")]
        [AllowAnonymous]
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> RegisterUser([FromQuery] CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await sender.Send(command, cancellationToken);
        }

        [HttpGet]
        public IActionResult CheckAuth()
        {
            return Ok();
        }
    }
}
