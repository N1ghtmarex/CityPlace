using Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.Services
{
    public class FileService(IMinioClient minio, IConfiguration configuration) : IFileService
    {
        public async Task<string> SaveFileAsync(string additionalPath, string fileName, IFormFile file, CancellationToken cancellationToken)
        {
            await using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);
                stream.Position = 0;

                await minio.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(configuration["MinIO:PicturesBucketName"])
                    .WithObject(fileName)
                    .WithStreamData(stream)
                    .WithObjectSize(file.Length)
                    .WithContentType(file.ContentType)
                );

                await stream.FlushAsync(cancellationToken);                
            }

            return fileName;
        }
    }
}
