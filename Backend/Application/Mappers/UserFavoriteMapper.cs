using Application.Locations.Dtos;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(LocationMapper))]
[UseStaticMapper(typeof(PictureMapper))]
public static partial class UserFavoriteMapper
{
    [MapProperty(nameof(UserFavorite.Location.Name), nameof(LocationViewModel.Name))]
    [MapProperty(nameof(UserFavorite.Location.Description), nameof(LocationViewModel.Description))]
    [MapProperty(nameof(UserFavorite.Location.Type), nameof(LocationViewModel.Type))]
    [MapProperty(nameof(UserFavorite.Location.LocationPictures), nameof(LocationViewModel.Pictures))]
    [MapProperty(nameof(UserFavorite.Location.Latitude), nameof(LocationViewModel.Latitude))]
    [MapProperty(nameof(UserFavorite.Location.Longitude), nameof(LocationViewModel.Longitude))]
    public static partial LocationListViewModel ProjectToListViewModel(UserFavorite source);

    public static partial List<LocationListViewModel> MapToListViewModel(List<UserFavorite> source);


    [MapValue(nameof(UserFavorite.Id), Use = nameof(@GeneralMapper.GenerateId))]
    [MapValue(nameof(UserFavorite.CreatedAt), Use = nameof(@GeneralMapper.SetCreatedAt))]
    [MapProperty(nameof(userId), nameof(UserFavorite.UserId))]
    public static partial UserFavorite MapToEntity(Ulid userId, Ulid locationId);
}
