using Microsoft.AspNetCore.Http;

namespace CloupardTask.Service.Interfaces.Commons
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile image);
        Task<bool> DeleteImageAsync(string relativeFilePath);
    }
}
