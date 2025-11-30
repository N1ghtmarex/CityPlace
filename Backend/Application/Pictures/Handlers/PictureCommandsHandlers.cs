using Abstractions;
using Application.Abstractions.Models;
using Application.Mappers;
using Application.Pictures.Commands;
using Core.Exceptions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Pictures.Handlers
{
    internal class PictureCommandsHandlers(ApplicationDbContext dbContext, IFileService fileService, IUserService userService, ICurrentHttpContextAccessor httpContext) :
        IRequestHandler<UploadLocationPictureCommand, CreatedOrUpdatedEntityViewModel<Ulid>>
    {
        public async Task<CreatedOrUpdatedEntityViewModel<Ulid>> Handle(UploadLocationPictureCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByExternalIdAsync(httpContext.IdentityUserId, cancellationToken);

            var location = await dbContext.Locations
                .SingleOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken)
                ?? throw new ObjectNotFoundException($"Локация с идентификатором \"{request.LocationId}\" не найдена!");
            
            var picture = PictureMapper.MapToEntity(string.Empty, user.Id);

            picture.Path = await fileService.SaveFileAsync(user.Id.ToString(), picture.Id.ToString(), request.Body.File, cancellationToken);

            var createdPicture = await dbContext.AddAsync(picture, cancellationToken);

            var locationPictureBind = PictureMapper.MapToBind(locationId: location.Id, pictureId: picture.Id);

            await dbContext.AddAsync(locationPictureBind, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatedOrUpdatedEntityViewModel(createdPicture.Entity.Id);
        }
    }
}
