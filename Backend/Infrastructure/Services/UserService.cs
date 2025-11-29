using Abstractions;
using Core.Exceptions;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserService(ApplicationDbContext dbContext, ICurrentHttpContextAccessor httpContext) : IUserService
    {
        public async Task<User> GetUserByExternalIdAsync(string? id, CancellationToken cancellationToken)
        {
            var externalId = id != null ? Guid.Parse(id) : throw new BusinessLogicException("Не задан идентификатор пользователя внешней системы!");

            var user = await dbContext.Users
                .SingleOrDefaultAsync(x => x.ExternalUserId == externalId, cancellationToken)
                ?? throw new ObjectNotFoundException($"Пользователь с идентификатором внешней системы \"{httpContext.IdentityUserId}\" не найден!");

            return user;
        }
    }
}
