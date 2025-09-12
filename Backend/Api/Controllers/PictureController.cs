using Application.Abstractions.Models;
using Application.Pictures.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/picture")]
    public class PictureController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Публикация изображения
        /// </summary>
        /// <param name="command">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpPost("locationId/{LocationId}")]
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> UploadPicture([FromQuery] UploadLocationPictureCommand command, CancellationToken cancellationToken)
        {
            return await sender.Send(command, cancellationToken);
        }
    }
}
