using Application.Addresses.Dtos;
using Application.Locations.Dtos;
using Domain.Entities;
using Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
public static partial class LocationMapper
{
    public static partial IQueryable<LocationViewModel> ProjectToViewModels(this IQueryable<Location> q, AddressViewModel address);
    public static partial IQueryable<LocationListViewModel> ProjectToListViewModels(this IQueryable<Location> q, AddressViewModel address);

    [MapValue(nameof(Location.Id), Use = nameof(GenerateId))]
    [MapValue(nameof(Location.CreatedAt), Use = nameof(SetCreatedAt))]
    public static partial Location MapToEntity(CreateLocationModel source, Address address, LocationType type);

    [NamedMapping("GenerateId")]
    static Ulid GenerateId() => Ulid.NewUlid();

    [NamedMapping("SetCreatedAt")]
    static DateTimeOffset SetCreatedAt() => DateTime.UtcNow;
}
