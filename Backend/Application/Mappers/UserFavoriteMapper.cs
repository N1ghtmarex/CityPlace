using Application.Locations.Dtos;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(AddressMapper))]
[UseStaticMapper(typeof(LocationMapper))]
[UseStaticMapper(typeof(PictureMapper))]
public static partial class UserFavoriteMapper
{
    [MapProperty(nameof(UserFavorite.Location.Name), nameof(LocationViewModel.Name))]
    [MapProperty(nameof(UserFavorite.Location.Description), nameof(LocationViewModel.Description))]
    [MapProperty(nameof(UserFavorite.Location.Type), nameof(LocationViewModel.Type))]
    [MapProperty(nameof(UserFavorite.Location.Address), nameof(LocationViewModel.Address))]
    [MapProperty(nameof(UserFavorite.Location.LocationPictures), nameof(LocationViewModel.Pictures))]
    public static partial LocationListViewModel ProjectToListViewModel(UserFavorite source);

    public static partial List<LocationListViewModel> MapToListViewModel(List<UserFavorite> source);
}
