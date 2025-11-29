using Application.Abstractions.Models;
using Application.Mappers;
using Application.Users.Commands;
using Core.Exceptions;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Keycloak.Abstractions;
using Keycloak.Models.KeycloakApiModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

            var credentials = new List<Credentials>
            {
                new()
                {
                    Type = "password",
                    Value = request.Body.Password,
                    Temporary = false
                }
            };

            var createKeycloakUserModel = UserMapper.MapToKeycloakUser(request.Body, credentials);

            var keycloakUserId = Guid.Parse(await identityService.CreateUserAsync(createKeycloakUserModel, cancellationToken));

            var userToCreate = UserMapper.MapToEntity(request.Body, externalUserId: keycloakUserId, role: UserRole.Admin);

            var createdUser = await dbContext.AddAsync(userToCreate, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedOrUpdatedEntityViewModel(createdUser.Entity.Id);
        }
    }
}
