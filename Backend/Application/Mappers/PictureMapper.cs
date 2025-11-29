using Application.Pictures.Dtos;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(GeneralMapper))]
public static partial class PictureMapper
{
    [MapProperty(nameof(LocationPictureBind.Picture.Id), nameof(PictureViewModel.Id))]
    [MapProperty(nameof(LocationPictureBind.Picture.Path), nameof(PictureViewModel.Path))]
    [MapProperty(nameof(LocationPictureBind.Picture.UserId), nameof(PictureViewModel.UserId))]
    [MapProperty(nameof(LocationPictureBind.Picture.CreatedAt), nameof(PictureViewModel.CreatedAt))]
    [MapProperty(nameof(LocationPictureBind.IsAvatar), nameof(PictureViewModel.IsAvatar))]
    public static partial PictureViewModel MapToViewModel(LocationPictureBind locationPictureBind);

    [MapValue(nameof(Picture.Id), Use = nameof(@GeneralMapper.GenerateId))]
    [MapValue(nameof(Picture.CreatedAt), Use = nameof(@GeneralMapper.SetCreatedAt))]
    [MapValue(nameof(Picture.IsArchive), false)]
    [MapValue(nameof(Picture.Path), nameof(path))]
    public static partial Picture MapToEntity(string path, Ulid userId);

    [MapValue(nameof(LocationPictureBind.Id), Use = nameof(@GeneralMapper.GenerateId))]
    [MapValue(nameof(LocationPictureBind.CreatedAt), Use = nameof(@GeneralMapper.SetCreatedAt))]
    [MapValue(nameof(LocationPictureBind.IsArchive), false)]
    [MapValue(nameof(LocationPictureBind.IsAvatar), false)]
    [MapProperty(nameof(locationId), nameof(LocationPictureBind.LocationId))]
    public static partial LocationPictureBind MapToBind(Ulid locationId, Ulid pictureId);
}
