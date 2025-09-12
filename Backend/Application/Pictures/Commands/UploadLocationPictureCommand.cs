using Application.Abstractions.Models;
using Application.Pictures.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Pictures.Commands
{
    public class UploadLocationPictureCommand : IRequest<CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        [FromRoute]
        public required Ulid LocationId { get; init; }

        [FromForm]
        public required UploadPictureModel Body { get; init; }
    }
}
