using Application.BaseModels;
using Application.Locations.Dtos;
using Core.EntityFramework.Features.SearchPagination.Models;
using MediatR;

namespace Application.Locations.Queries
{
    public class GetLocationsListQuery : SearchablePagedQuery, IRequest<PagedResult<LocationListViewModel>>
    {
    }
}
