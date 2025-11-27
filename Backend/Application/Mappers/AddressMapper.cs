using Application.Addresses.Dtos;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
public static partial class AddressMapper
{
    public static partial IQueryable<AddressViewModel> ProjectToViewModel(this IQueryable<Address> q);
    public static partial IQueryable<AddressListViewModel> ProjectToListViewModel(this IQueryable<Address> q);
    public static partial AddressViewModel MapToViewModel(Address q);
}
