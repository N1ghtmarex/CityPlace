using Api.StartupConfigurations;
using Application.Abstractions.Models;
using Application.Users.Commands;
using Application.Users.Dtos;
using Application.Users.Queries;
using Core.EntityFramework.Features.SearchPagination.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="command">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpPost("registration")]
        [AllowAnonymous]
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> RegisterUser([FromQuery] CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await sender.Send(command, cancellationToken);
        }

        /// <summary>
        /// Получение конкретного пользователя
        /// </summary>
        /// <param name="query">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpGet("{UserId}")]
        public async Task<UserViewModel> GetUser([FromQuery] GetUserQuery query, CancellationToken cancellationToken)
        {
            return await sender.Send(query, cancellationToken);
        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <param name="query">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResult<UserListViewModel>> GetUsersList([FromQuery] GetUsersListQuery query, CancellationToken cancellationToken)
        {
            return await sender.Send(query, cancellationToken);
        }

        /// <summary>
        /// Получение информации о текущем пользователе
        /// </summary>
        /// <param name="query">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<UserViewModel> GetCurrentUser([FromQuery] GetCurrentUserQuery query, CancellationToken cancellationToken)
        {
            return await sender.Send(query, cancellationToken);
        }
    }
}
