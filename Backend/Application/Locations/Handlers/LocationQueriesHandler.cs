using Application.Addresses.Dtos;
using Application.Locations.Dtos;
using Application.Locations.Queries;
using Core.EntityFramework.Features.SearchPagination;
using Core.EntityFramework.Features.SearchPagination.Models;
using Core.Exceptions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Locations.Handlers
{
    internal class LocationQueriesHandler(ApplicationDbContext dbContext) :
        IRequestHandler<GetLocationQuery, LocationViewModel>, IRequestHandler<GetLocationsListQuery, PagedResult<LocationListViewModel>>
    {
        public async Task<LocationViewModel> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            var location = await dbContext.Locations
                .Include(x => x.Address)
                .Select(x => new LocationViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    AddressId = x.AddressId,
                    Address = new AddressViewModel
                    {
                        Region = x.Address!.Region,
                        District = x.Address!.District,
                        Appartment = x.Address!.Appartment,
                        House = x.Address!.House,
                        PlanningStructure = x.Address!.PlanningStructure,
                        Settlement = x.Address!.Settlement
                    },
                    Type = x.Type
                })
                .SingleOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken)
                ?? throw new ObjectNotFoundException($"Локация с идентификатором \"{request.LocationId}\" не найдена!");

            return location;
        }

        public async Task<PagedResult<LocationListViewModel>> Handle(GetLocationsListQuery request, CancellationToken cancellationToken)
        {
            var locationQuery = dbContext.Locations
                .Where(x => !x.IsArchive)
                .Include(x => x.Address)
                .OrderBy(x => x.Name)
                .ApplySearch(request, x => x.Name);

            var result = await locationQuery
                .ApplyPagination(request)
                .Select(x => new LocationListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    AddressId = x.AddressId,
                    Address = new AddressViewModel
                    {
                        Region = x.Address!.Region,
                        District = x.Address!.District,
                        Appartment = x.Address!.Appartment,
                        House = x.Address!.House,
                        PlanningStructure = x.Address!.PlanningStructure,
                        Settlement = x.Address!.Settlement
                    },
                    Type = x.Type
                })
                .ToListAsync(cancellationToken);

            return result.AsPagedResult(request, await locationQuery.CountAsync(cancellationToken));
        }
    }
}
