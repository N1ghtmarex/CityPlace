using Application.Addresses.Dtos;
using Application.BaseModels;
using Core.EntityFramework.Features.SearchPagination.Models;
using MediatR;

namespace Application.Addresses.Queries
{
    public class GetAddressesListQuery : SearchablePagedQuery, IRequest<PagedResult<AddressListViewModel>>
    {
    }
}
