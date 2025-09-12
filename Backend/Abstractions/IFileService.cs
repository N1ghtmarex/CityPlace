using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Abstractions
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(string additionalPath, string fileName, IFormFile file, CancellationToken cancellationToken);
    }
}
