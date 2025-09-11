using Application.Addresses.Dtos;
using Application.Addresses.Queries;
using Core.EntityFramework.Features.SearchPagination;
using Core.EntityFramework.Features.SearchPagination.Models;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Addresses.Handlers
{
    internal class AddressQueriesHandlers(ApplicationDbContext dbContext) :
        IRequestHandler<GetAddressesListQuery, PagedResult<AddressListViewModel>>
    {
        public async Task<PagedResult<AddressListViewModel>> Handle(GetAddressesListQuery request, CancellationToken cancellationToken)
        {
            var addressQuery = dbContext.Addresses
                .OrderBy(x => x.Region)
                .ThenBy(x => x.Settlement)
                .ThenBy(x => x.District)
                .ThenBy(x => x.PlanningStructureFiasId)
                .ThenBy(x => x.House)
                .ThenBy(x => x.Appartment)
                .ApplySearch(request, x => x.Region, x => x.Settlement, x => x.District, x => x.PlanningStructureFiasId, x => x.House, x => x.Appartment);

            var result = await addressQuery
                .ApplyPagination(request)
                .Select(x => new AddressListViewModel
                {
                    Id = x.Id,
                    Region = x.Region,
                    Settlement = x.Settlement,
                    District = x.District,
                    PlanningStructure = x.PlanningStructure,
                    House = x.House,
                    Appartment = x.Appartment
                })
                .ToListAsync(cancellationToken);

            return result.AsPagedResult(request, await addressQuery.CountAsync(cancellationToken));
        }
    }
}
