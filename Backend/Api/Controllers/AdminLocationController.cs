using Application.Abstractions.Models;
using Application.Locations.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("/api/admin/locations")]
    public class AdminLocationController(ISender sender) : ControllerBase
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
        /// Установление аватара локации
        /// </summary>
        /// <param name="command">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpPut("{LocationId}/set-avatar/{PictureId}")]
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> SetLocationAvatar([FromQuery] SetLocationAvatarCommand command, CancellationToken cancellationToken)
        {
            return await sender.Send(command, cancellationToken);
        }
    }
}
