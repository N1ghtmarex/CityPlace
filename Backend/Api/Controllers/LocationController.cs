using Application.Abstractions.Models;
using Application.Locations.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/locations")]
    public class LocationController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> CreateLocation([FromQuery] CreateLocationCommand command, CancellationToken cancellationToken)
        {
            return await sender.Send(command, cancellationToken);
        }
    }
}
