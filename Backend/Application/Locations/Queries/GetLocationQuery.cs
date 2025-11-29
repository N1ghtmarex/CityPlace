using Application.Locations.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Locations.Queries
{
    public class GetLocationQuery : IRequest<LocationViewModel>
    {
        /// <summary>
        /// Идентификатор локации
        /// </summary>
        [FromRoute]
        public required Ulid LocationId { get; init; }
    }
}
