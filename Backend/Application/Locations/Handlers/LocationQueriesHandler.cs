using Abstractions;
using Application.Locations.Dtos;
using Application.Locations.Queries;
using Application.Mappers;
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
                .AsNoTracking()
                .Include(x => x.LocationPictures)
                    .ThenInclude(x => x.Picture)
                .ProjectToViewModel()
                .SingleOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken)
                ?? throw new ObjectNotFoundException($"Локация с идентификатором \"{request.LocationId}\" не найдена!");

            return location;
        }

        public async Task<PagedResult<LocationListViewModel>> Handle(GetLocationsListQuery request, CancellationToken cancellationToken)
        {
            var locationQuery = dbContext.Locations
                .AsNoTracking()
                .Where(x => !x.IsArchive)
                .Include(x => x.LocationPictures)
                    .ThenInclude(x => x.Picture)
                .OrderBy(x => x.Name)
                .ApplySearch(request, x => x.Name);

            var result = await locationQuery
                .ApplyPagination(request)
                .ProjectToListViewModels()
                .ToListAsync(cancellationToken);

            return result.AsPagedResult(request, await locationQuery.CountAsync(cancellationToken));
        }

        public async Task<PagedResult<LocationListViewModel>> Handle(GetFavoriteLocationListQuery request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByExternalIdAsync(httpContext.IdentityUserId, cancellationToken);

            var userFavoriteQuery = dbContext.UserFavorites
                .AsNoTracking()
                .Include(x => x.Location)
                .Include(x => x.Location.LocationPictures)
                    .ThenInclude(x => x.Picture)
                .Where(x => x.UserId == user.Id)
                .OrderBy(x => x.Location!.Name)
                .ApplySearch(request, x => x.Location!.Name);

            var userFavoriteList = await userFavoriteQuery
                .ApplyPagination(request)
                .ToListAsync(cancellationToken);

            var result = UserFavoriteMapper.MapToListViewModel(userFavoriteList);

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
