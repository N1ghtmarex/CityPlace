using Application.Abstractions.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Users.Queries
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [FromRoute]
        public required Ulid UserId { get; set; }
    }
}
