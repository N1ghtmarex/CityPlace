using Application.Abstractions.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Locations.Commands
{
    public class AddOrRemoveFavoriteLocationCommand : IRequest<CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        [FromRoute]
        public required Ulid LocationId { get; set; }
    }
}
