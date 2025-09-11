using Application.Abstractions.Models;
using Application.Locations.Commands;
using Application.Locations.Dtos;
using Application.Locations.Queries;
using Core.EntityFramework.Features.SearchPagination.Models;
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
        /// <summary>
        /// Добавление локации
        /// </summary>
        /// <param name="command">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> CreateLocation([FromQuery] CreateLocationCommand command, CancellationToken cancellationToken)
        {
            return await sender.Send(command, cancellationToken);
        }

        /// <summary>
        /// Получение конкретной локации
        /// </summary>
        /// <param name="query">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpGet("{LocationId}")]
        public async Task<LocationViewModel> GetLocation([FromQuery] GetLocationQuery query, CancellationToken cancellationToken)
        {
            return await sender.Send(query, cancellationToken);
        }

        /// <summary>
        /// Получение списка локаций
        /// </summary>
        /// <param name="query">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResult<LocationListViewModel>> GetLocationsList([FromQuery] GetLocationsListQuery query, CancellationToken cancellationToken)
        {
            return await sender.Send(query, cancellationToken);
        }

        /// <summary>
        /// Обновление локации
        /// </summary>
        /// <param name="command">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpPut("{LocationId}")]
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> UpdateLocation([FromQuery] UpdateLocationCommand command, CancellationToken cancellationToken)
        {
            return await sender.Send(command, cancellationToken);
        }

        /// <summary>
        /// Добавление или удаление локации из избранного
        /// </summary>
        /// <param name="command">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpPost("favorite/{LocationId}")]
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> AddOrRemoveFavoriteLocation([FromQuery] AddOrRemoveFavoriteLocationCommand command,
            CancellationToken cancellationToken)
        {
            return await sender.Send(command, cancellationToken);
        }

        /// <summary>
        /// Получение избранных локаций
        /// </summary>
        /// <param name="query">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpGet("favorite")]
        public async Task<PagedResult<LocationListViewModel>> GetFavoriteLocations([FromQuery] GetFavoriteLocationListQuery query, CancellationToken cancellationToken)
        {
            return await sender.Send(query, cancellationToken);
        }
    }
}
