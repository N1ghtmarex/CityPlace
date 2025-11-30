using Microsoft.AspNetCore.Http;

namespace Application.Pictures.Dtos
{
    public class UploadPictureModel
    {
        public required IFormFile File { get; init; }
    }
}
