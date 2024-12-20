using CloupardTask.Service.Helpers;
using CloupardTask.Service.Interfaces.Commons;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CloupardTask.Service.Services.Commons
{
    public class FileService : IFileService
    {
        private readonly string _basePath;
        private readonly string _imageFolderName = "Images";
        public FileService(IWebHostEnvironment environment)
        {
            _basePath = environment.WebRootPath;
        }
        public Task<bool> DeleteImageAsync(string relativeFilePath)
        {
            string absoluteFilePath = Path.Combine(_basePath, relativeFilePath);

            if (!File.Exists(absoluteFilePath)) return Task.FromResult(false);

            try
            {
                File.Delete(absoluteFilePath);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            string fileName = ImageHelper.MakeImageName(image.FileName);
            string partPath = Path.Combine(_imageFolderName, fileName);
            string path = Path.Combine(_basePath, partPath);

            var stream = File.Create(path);
            await image.CopyToAsync(stream);
            stream.Close();
            return partPath;
        }
    }
}
