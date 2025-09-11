using Application.Abstractions.Models;
using Application.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Users.Commands
{
    public class CreateUserCommand : IRequest<CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        /// <summary>
        /// Тело запроса
        /// </summary>
        [FromBody]
        public required CreateUserModel Body { get; set; }
    }
}
