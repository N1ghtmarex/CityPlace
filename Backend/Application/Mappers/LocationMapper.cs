using Application.Addresses.Dtos;
using Application.Locations.Dtos;
using Domain.Entities;
using Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(AddressMapper))]
public static partial class LocationMapper
{
    public static partial IQueryable<LocationViewModel> ProjectToViewModel(this IQueryable<Location> q);
    public static partial IQueryable<LocationListViewModel> ProjectToListViewModels(this IQueryable<Location> q);

    [MapValue(nameof(Location.Id), Use = nameof(GenerateId))]
    [MapValue(nameof(Location.CreatedAt), Use = nameof(SetCreatedAt))]
    public static partial Location MapToEntity(CreateLocationModel source, Address address, LocationType type);

    [NamedMapping("GenerateId")]
    private static Ulid GenerateId()
    {
        return Ulid.NewUlid();
    }

    [NamedMapping("SetCreatedAt")]
    private static DateTimeOffset SetCreatedAt()
    {
        return DateTime.UtcNow;
    }
}
