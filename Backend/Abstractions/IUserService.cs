using Domain.Entities;

namespace Abstractions
{
    public interface IUserService
    {
        Task<User> GetUserByExternalIdAsync(string? id, CancellationToken cancellationToken);
    }
}
