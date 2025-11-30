using Application.Locations.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(GeneralMapper))]
[UseStaticMapper(typeof(PictureMapper))]
public static partial class LocationMapper
{
    public static partial IQueryable<LocationViewModel> ProjectToViewModel(this IQueryable<Location> q);


    [MapProperty(nameof(Location.LocationPictures), nameof(LocationViewModel.Pictures))]
    public static partial LocationViewModel MapToViewModel(Location source);


    [MapProperty(nameof(Location.LocationPictures), nameof(LocationListViewModel.Pictures))]
    public static partial LocationListViewModel MapToListViewModel(Location source);


    public static partial IQueryable<LocationListViewModel> ProjectToListViewModels(this IQueryable<Location> q);


    [MapValue(nameof(Location.Id), Use = nameof(@GeneralMapper.GenerateId))]
    [MapValue(nameof(Location.CreatedAt), Use = nameof(@GeneralMapper.SetCreatedAt))]

    public static partial Location MapToEntity(CreateLocationModel source, LocationType type);


    public static Location MapToEntity(UpdateLocationModel newData, Location entity)
    {
        entity.Name = newData.Name;
        entity.Description = newData.Description;
        entity.Type = newData.LocationType;
        entity.Longitude = newData.Longitude;
        entity.Latitude = newData.Latitude;

        return entity;
    }
}
