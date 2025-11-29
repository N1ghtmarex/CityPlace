using Application.Abstractions.Models;
using Application.Locations.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Locations.Commands
{
    public class SetLocationAvatarCommand : IRequest<CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        [FromRoute]
        public required Ulid LocationId { get; init; }

        [FromRoute]
        public required Ulid PictureId { get; init; }
    }
}
