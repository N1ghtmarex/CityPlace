using Application.Abstractions.Models;
using Application.Locations.Commands;
using Core.Exceptions;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Locations.Handlers
{
    internal class LocationCommandsHandler(ApplicationDbContext dbContext) :
        IRequestHandler<CreateLocationCommand, CreatedOrUpdatedEntityViewModel<Ulid>>, IRequestHandler<UpdateLocationCommand, CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var address = await dbContext.Addresses
                .SingleOrDefaultAsync(x => x.Region == request.Body.Address.Region && x.District == request.Body.Address.District
                    && x.Settlement == request.Body.Address.Settlement && x.PlanningStructure == request.Body.Address.PlanningStructure && x.House == request.Body.Address.House
                    && x.Appartment == request.Body.Address.Appartment, cancellationToken);

            if (address == null)
            {
                var addressToCreate = new Address
                {
                    Id = Ulid.NewUlid(),
                    Region = request.Body.Address.Region,
                    RegionFiasId = Ulid.NewUlid().ToString(),
                    District = request.Body.Address.District,
                    DistrictFiasId = Ulid.NewUlid().ToString(),
                    Settlement = request.Body.Address.Settlement,
                    SettlementFiasId = Ulid.NewUlid().ToString(),
                    PlanningStructure = request.Body.Address.PlanningStructure,
                    PlanningStructureFiasId = Ulid.NewUlid().ToString(),
                    House = request.Body.Address.House,
                    HouseFiasId = Ulid.NewUlid().ToString(),
                    Appartment = request.Body.Address.Appartment,
                    AppartmentFiasId = Ulid.NewUlid().ToString()
                };

                var createdAddress = await dbContext.AddAsync(addressToCreate, cancellationToken);
                address = createdAddress.Entity;
            }

            var existLocation = await dbContext.Locations
                .SingleOrDefaultAsync(x => x.Name == request.Body.Name || x.Address == address, cancellationToken);

            if (existLocation != null)
            {
                throw new ObjectExistsException("Локация с таким названием или адресом уже существует!");
            }

            var locationToCreate = new Location
            {
                Id = Ulid.NewUlid(),
                Name = request.Body.Name,
                Description = request.Body.Description,
                Type = request.Body.LocationType,
                AddressId = address.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var createdLocation = await dbContext.AddAsync(locationToCreate, cancellationToken);
            await dbContext.SaveChangesAsync();

            return new CreatedOrUpdatedEntityViewModel(createdLocation.Entity.Id);
        }

        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var existsLocation = await dbContext.Locations
                .SingleOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken);

            if (existsLocation == null)
            {
                throw new ObjectNotFoundException($"Локация с идентификатором \"{request.LocationId}\" не найдена!");
            }

            var address = await dbContext.Addresses
                .SingleOrDefaultAsync(x => x.Region == request.Body.Address.Region && x.District == request.Body.Address.District
                    && x.Settlement == request.Body.Address.Settlement && x.PlanningStructure == request.Body.Address.PlanningStructure
                    && x.House == request.Body.Address.House && x.Appartment == request.Body.Address.Appartment, cancellationToken);

            if (address == null)
            {
                var addressToCreate = new Address
                {
                    Id = Ulid.NewUlid(),
                    Region = request.Body.Address.Region,
                    RegionFiasId = Ulid.NewUlid().ToString(),
                    District = request.Body.Address.District,
                    DistrictFiasId = Ulid.NewUlid().ToString(),
                    Settlement = request.Body.Address.Settlement,
                    SettlementFiasId = Ulid.NewUlid().ToString(),
                    PlanningStructure = request.Body.Address.PlanningStructure,
                    PlanningStructureFiasId = Ulid.NewUlid().ToString(),
                    House = request.Body.Address.House,
                    HouseFiasId = Ulid.NewUlid().ToString(),
                    Appartment = request.Body.Address.Appartment,
                    AppartmentFiasId = Ulid.NewUlid().ToString()
                };

                var createdAddress = await dbContext.AddAsync(addressToCreate, cancellationToken);
                address = createdAddress.Entity;
            }

            existsLocation.Name = request.Body.Name;
            existsLocation.Description = request.Body.Description;
            existsLocation.Type = request.Body.LocationType;
            existsLocation.AddressId = address.Id;

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedOrUpdatedEntityViewModel(existsLocation.Id);
        }
    }
}
