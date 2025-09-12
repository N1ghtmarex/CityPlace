using Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string directory = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.ToString() ?? string.Empty);

        public async Task<string> SaveFileAsync(string additionalPath, string fileName, IFormFile file, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine("Uploads", additionalPath);

            var fullPath = Path.Combine(directory, filePath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            await using (var stream = new FileStream(Path.Combine(fullPath, $"{fileName}{Path.GetExtension(file.FileName)}"), FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
                await stream.FlushAsync(cancellationToken);
            }

            return Path.Combine(filePath, $"{fileName}{Path.GetExtension(file.FileName)}");
        }
    }
}
