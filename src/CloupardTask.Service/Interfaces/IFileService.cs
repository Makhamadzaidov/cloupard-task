using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloupardTask.Service.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile image);
        Task<bool> DeleteImageAsync(string relativeFilePath);
    }
}
