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
            var externalUserResult = await identityService.GetUserByUsername(request.Body.Username, cancellationToken);
            var externalUser = externalUserResult.Count == 0 ? null : externalUserResult[0];

            var userWithSameUsername = await dbContext.Users.SingleOrDefaultAsync(x => x.Username == request.Body.Username, cancellationToken);

            if (externalUser != null && userWithSameUsername != null)
            {
                throw new BusinessLogicException($"Пользователь с именем \"{userWithSameUsername.Username}\" уже существует!");
            }
            else if (externalUser == null && userWithSameUsername != null)
            {
                dbContext.Remove(userWithSameUsername);
            }
            else if (externalUser != null && userWithSameUsername == null)
            {
                await identityService.DeleteUserAsync(externalUser.Id, cancellationToken);
            }

            var createKeycloakUserModel = new CreateKeyckloakUserModel
            {
                UserName = request.Body.Username,
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

            var keycloakUserId = Guid.Parse(await identityService.CreateUserAsync(createKeycloakUserModel, cancellationToken));

            var userToCreate = new User
            {
                ExternalUserId = keycloakUserId,
                Id = Ulid.NewUlid(),
                Username = request.Body.Username,
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow
            };

            var userToCreate = new User
            {
                Id = Ulid.NewUlid(),
                ExternalUserId = Guid.Parse(keycloakUserId),
                Username = request.Body.Username,
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await dbContext.AddAsync(userToCreate, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedOrUpdatedEntityViewModel(createdUser.Entity.Id);
        }
    }
}
