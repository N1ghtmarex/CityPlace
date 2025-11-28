using Abstractions;
using Application.Abstractions.Models;
using Application.Locations.Commands;
using Application.Mappers;
using Core.Exceptions;
using Domain;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Locations.Handlers
{
    internal class LocationCommandsHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor httpContext, IUserService userService) :
        IRequestHandler<CreateLocationCommand, CreatedOrUpdatedEntityViewModel<Ulid>>, IRequestHandler<UpdateLocationCommand, CreatedOrUpdatedEntityViewModel<Ulid>>,
        IRequestHandler<AddOrRemoveFavoriteLocationCommand, CreatedOrUpdatedEntityViewModel<Ulid>>, 
        IRequestHandler<SetLocationAvatarCommand, CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByExternalIdAsync(httpContext.IdentityUserId, cancellationToken);

            if (user.Role != UserRole.Admin)
            {
                throw new ForbiddenException("Только администратор может добавлять новые локации!");
            }

            var address = await dbContext.Addresses
                .SingleOrDefaultAsync(x => x.Region == request.Body.Address.Region && x.District == request.Body.Address.District
                    && x.Settlement == request.Body.Address.Settlement && x.PlanningStructure == request.Body.Address.PlanningStructure && x.House == request.Body.Address.House
                    && x.Appartment == request.Body.Address.Appartment, cancellationToken);

            if (address == null)
            {
                var addressToCreate = AddressMapper.MapToEntity(request.Body.Address);

                var createdAddress = await dbContext.AddAsync(addressToCreate, cancellationToken);
                address = createdAddress.Entity;
            }

            var existLocation = await dbContext.Locations
                .SingleOrDefaultAsync(x => x.Name == request.Body.Name || x.Address == address, cancellationToken);

            if (existLocation != null)
            {
                throw new ObjectExistsException("Локация с таким названием или адресом уже существует!");
            }

            var locationToCreate = LocationMapper.MapToEntity(request.Body, address, request.Body.LocationType);

            var createdLocation = await dbContext.AddAsync(locationToCreate, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedOrUpdatedEntityViewModel(createdLocation.Entity.Id);
        }

        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByExternalIdAsync(httpContext.IdentityUserId, cancellationToken);

            if (user.Role != UserRole.Admin)
            {
                throw new ForbiddenException("Только администратор может редактировать локации!");
            }

            var existsLocation = await dbContext.Locations
                .SingleOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken)
                ?? throw new ObjectNotFoundException($"Локация с идентификатором \"{request.LocationId}\" не найдена!");

            var address = await dbContext.Addresses
                .SingleOrDefaultAsync(x => x.Region == request.Body.Address.Region && x.District == request.Body.Address.District
                    && x.Settlement == request.Body.Address.Settlement && x.PlanningStructure == request.Body.Address.PlanningStructure
                    && x.House == request.Body.Address.House && x.Appartment == request.Body.Address.Appartment, cancellationToken);

            if (address == null)
            {
                var addressToCreate = AddressMapper.MapToEntity(request.Body.Address);

                var createdAddress = await dbContext.AddAsync(addressToCreate, cancellationToken);
                address = createdAddress.Entity;
            }

            existsLocation = LocationMapper.MapToEntity(request.Body, existsLocation, address);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedOrUpdatedEntityViewModel(existsLocation.Id);
        }

        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> Handle(AddOrRemoveFavoriteLocationCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByExternalIdAsync(httpContext.IdentityUserId, cancellationToken);

            var location = await dbContext.Locations
                .SingleOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken)
                ?? throw new ObjectNotFoundException($"Локация с идентификатором \"{request.LocationId}\" не найдена!");

            var userLocationBind = await dbContext.UserFavorites
                .SingleOrDefaultAsync(x => x.UserId == user.Id && x.LocationId == location.Id, cancellationToken);

            if (userLocationBind == null)
            {
                
                var userLocationBindToCreate = UserFavoriteMapper.MapToEntity(userId: user.Id, locationId: location.Id);

                var createdUserLocationBind = await dbContext.AddAsync(userLocationBindToCreate, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                return new CreatedOrUpdatedEntityViewModel(createdUserLocationBind.Entity.Id);
            }
            else
            {
                dbContext.Remove(userLocationBind);
                await dbContext.SaveChangesAsync(cancellationToken);

                return new CreatedOrUpdatedEntityViewModel(userLocationBind.Id);
            }
        }

        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> Handle(SetLocationAvatarCommand request, CancellationToken cancellationToken)
        {
            var location = await dbContext.Locations
                .Include(x => x.LocationPictures)
                .SingleOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken)
                ?? throw new ObjectNotFoundException($"Локация с идентификатором \"{request.LocationId}\" не найдена!");

            var locationPicture = location.LocationPictures!
                .SingleOrDefault(x => x.PictureId == request.PictureId)
                ?? throw new ObjectNotFoundException(
                    $"Изображение с идентификатором \"{request.PictureId}\" не принадлежит локации с идентификатором \"{request.LocationId}\"!"
                    );

            var currentLocationAvatar = location.LocationPictures!
                .SingleOrDefault(x => x.IsAvatar);

            if (currentLocationAvatar != null)
            {
                currentLocationAvatar.IsAvatar = false;
            }

            locationPicture.IsAvatar = true;

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedOrUpdatedEntityViewModel(location.Id);
        }
    }
}
