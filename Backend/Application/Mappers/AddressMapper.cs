using Application.Addresses.Dtos;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(GeneralMapper))]
public static partial class AddressMapper
{
    public static partial IQueryable<AddressViewModel> ProjectToViewModel(this IQueryable<Address> q);
    public static partial IQueryable<AddressListViewModel> ProjectToListViewModel(this IQueryable<Address> q);
    public static partial AddressViewModel MapToViewModel(Address q);

    [MapValue(nameof(Address.Id), Use = nameof(@GeneralMapper.GenerateId))]
    [MapValue(nameof(Address.RegionFiasId), Use = nameof(@GeneralMapper.GenerateStringId))]
    [MapValue(nameof(Address.DistrictFiasId), Use = nameof(@GeneralMapper.GenerateStringId))]
    [MapValue(nameof(Address.SettlementFiasId), Use = nameof(@GeneralMapper.GenerateStringId))]
    [MapValue(nameof(Address.PlanningStructureFiasId), Use = nameof(@GeneralMapper.GenerateStringId))]
    [MapValue(nameof(Address.HouseFiasId), Use = nameof(@GeneralMapper.GenerateStringId))]
    [MapValue(nameof(Address.AppartmentFiasId), Use = nameof(@GeneralMapper.GenerateStringId))]
    public static partial Address MapToEntity(CreateAddressModel source);
}
