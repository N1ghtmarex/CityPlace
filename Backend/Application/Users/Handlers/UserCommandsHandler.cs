using Application.Abstractions.Models;
using Application.Users.Commands;
using Core.Exceptions;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Keycloak.Abstractions;
using Keycloak.Models.KeycloakApiModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Handlers
{
    internal class UserCommandsHandler(ApplicationDbContext dbContext, IIdentityService identityService) :
        IRequestHandler<CreateUserCommand, CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userWithSameUsername = await dbContext.Users.SingleOrDefaultAsync(x => x.Username == request.Body.Username, cancellationToken);
            if (userWithSameUsername != null)
            {
                throw new BusinessLogicException($"Пользователь с именем \"{userWithSameUsername.Username}\" уже существует!");
            }

            var userToCreate = new User
            {
                Id = Ulid.NewUlid(),
                Username = request.Body.Username,
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow
            };

            var createKeycloakUserModel = new CreateKeyckloakUserModel
            {
                UserName = userToCreate.Username,
                Email = request.Body.Email,
                FirstName = request.Body.FirstName,
                LastName = request.Body.LastName,
                IsEnabled = true,
                Credentials =
                [
                    new() {
                        Type = "password",
                        Value = request.Body.Password,
                        Temporary = false
                    }
                ]
            };

            var keycloakUserId = await identityService.CreateUserAsync(createKeycloakUserModel, cancellationToken);

            var createdUser = await dbContext.AddAsync(userToCreate, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedOrUpdatedEntityViewModel(createdUser.Entity.Id);
        }
    }
}
