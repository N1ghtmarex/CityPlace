using Application.Abstractions.Models;
using Application.Locations.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Locations.Commands
{
    public class UpdateLocationCommand : IRequest<CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        /// <summary>
        /// Идентификатор локации
        /// </summary>
        [FromRoute]
        public required Ulid LocationId { get; init; }

        /// <summary>
        /// Тело запроса
        /// </summary>
        [FromBody]
        public required UpdateLocationModel Body { get; init; }
    }
}
