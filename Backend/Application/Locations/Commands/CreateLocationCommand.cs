using Application.Abstractions.Models;
using Application.Locations.Dtos;
using MediatR;

namespace Application.Locations.Commands
{
    public class CreateLocationCommand : IRequest<CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        /// <summary>
        /// Модель запроса
        /// </summary>
        public required CreateLocationModel Body { get; init; }
    }
}
