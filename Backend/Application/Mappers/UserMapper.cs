using Application.Users.Dtos;
using Domain.Entities;
using Domain.Enums;
using Keycloak.Models.KeycloakApiModels;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(GeneralMapper))]
public static partial class UserMapper
{
    public static partial CreateKeyckloakUserModel MapToKeycloakUser(CreateUserModel source, List<Credentials> credentials);

    [MapValue(nameof(User.Id), Use = nameof(@GeneralMapper.GenerateId))]
    public static partial User MapToEntity(CreateUserModel source, Guid externalUserId, UserRole role);
    public static partial IQueryable<UserViewModel> ProjectToViewModel(this IQueryable<User> q);
    public static partial IQueryable<UserListViewModel> ProjectToListViewModel(this IQueryable<User> q);
}
