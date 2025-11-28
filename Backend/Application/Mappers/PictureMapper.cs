using Application.Pictures.Dtos;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
public static partial class PictureMapper
{
    [MapProperty(nameof(LocationPictureBind.Picture.Id), nameof(PictureViewModel.Id))]
    [MapProperty(nameof(LocationPictureBind.Picture.Path), nameof(PictureViewModel.Path))]
    [MapProperty(nameof(LocationPictureBind.Picture.UserId), nameof(PictureViewModel.UserId))]
    [MapProperty(nameof(LocationPictureBind.Picture.CreatedAt), nameof(PictureViewModel.CreatedAt))]
    [MapProperty(nameof(LocationPictureBind.IsAvatar), nameof(PictureViewModel.IsAvatar))]
    public static partial PictureViewModel MapToViewModel(LocationPictureBind locationPictureBind);
}
