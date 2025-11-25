using Abstractions;
using Application.Addresses.Dtos;
using Application.Locations.Dtos;
using Application.Locations.Queries;
using Application.Pictures.Dtos;
using Core.EntityFramework.Features.SearchPagination;
using Core.EntityFramework.Features.SearchPagination.Models;
using Core.Exceptions;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Application.Locations.Handlers
{
    internal class LocationQueriesHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor httpContext, IUserService userService,
            ILocationService locationService) :
        IRequestHandler<GetLocationQuery, LocationViewModel>, IRequestHandler<GetLocationsListQuery, PagedResult<LocationListViewModel>>,
        IRequestHandler<GetFavoriteLocationListQuery, PagedResult<LocationListViewModel>>, 
        IRequestHandler<GetLocationTypesQuery, List<LocationTypesViewModel>>
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
                    Address = new AddressViewModel
                    {
                        Id = x.AddressId,
                        Region = x.Address!.Region,
                        District = x.Address!.District,
                        Appartment = x.Address!.Appartment,
                        House = x.Address!.House,
                        PlanningStructure = x.Address!.PlanningStructure,
                        Settlement = x.Address!.Settlement
                    },
                    Type = locationService.GetDescription(x.Type),
                    Pictures = x.LocationPictures!.Select(p => new PictureViewModel
                    {
                        Id = p.PictureId,
                        IsAvatar = p.IsAvatar,
                        Path = p.Picture!.Path,
                        CreatedAt = p.CreatedAt,
                        UserId = p.Picture.UserId
                    })
                    .ToList()
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
                    Address = new AddressViewModel
                    {
                        Id = x.AddressId,
                        Region = x.Address!.Region,
                        District = x.Address!.District,
                        Appartment = x.Address!.Appartment,
                        House = x.Address!.House,
                        PlanningStructure = x.Address!.PlanningStructure,
                        Settlement = x.Address!.Settlement
                    },
                    Type = locationService.GetDescription(x.Type),
                    Pictures = x.LocationPictures!.Select(p => new PictureViewModel
                    {
                        Id = p.PictureId,
                        IsAvatar = p.IsAvatar,
                        Path = p.Picture!.Path,
                        CreatedAt = p.CreatedAt,
                        UserId = p.Picture.UserId
                    })
                    .ToList()
                })
                .ToListAsync(cancellationToken);

            return result.AsPagedResult(request, await locationQuery.CountAsync(cancellationToken));
        }

        public async Task<PagedResult<LocationListViewModel>> Handle(GetFavoriteLocationListQuery request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByExternalIdAsync(httpContext.IdentityUserId, cancellationToken);

            var userFavoriteQuery = dbContext.UserFavorites
                .Include(x => x.Location)
                .ThenInclude(x => x!.Address)
                .Where(x => x.UserId == user.Id)
                .OrderBy(x => x.Location!.Name)
                .ApplySearch(request, x => x.Location!.Name);

            var result = await userFavoriteQuery
                .ApplyPagination(request)
                .Select(x => new LocationListViewModel
                {
                    Id = x.LocationId,
                    Name = x.Location!.Name,
                    Description = x.Location.Description,
                    Type = locationService.GetDescription(x.Location.Type),
                    Address = new AddressViewModel
                    {
                        Id = x.Location.Address!.Id,
                        District = x.Location.Address.District,
                        Appartment = x.Location.Address.Appartment,
                        House = x.Location.Address.House,
                        PlanningStructure = x.Location.Address.PlanningStructure,
                        Region = x.Location.Address.Region,
                        Settlement = x.Location.Address.Settlement
                    }
                })
                .ToListAsync(cancellationToken);

            return result.AsPagedResult(request, await userFavoriteQuery.CountAsync(cancellationToken));
        }

        public Task<List<LocationTypesViewModel>> Handle(GetLocationTypesQuery request, CancellationToken cancellationToken)
        {
            var result = Enum.GetValues(typeof(LocationType))
                .Cast<LocationType>()
                .Select(x => new LocationTypesViewModel
                {
                    Value = (int)x,
                    Name = x.ToString(),
                    Description = locationService.GetDescription(x)
                })
                .OrderBy(x => x.Description)
                .ToList();

            return Task.FromResult(result);
        }
    }
}
