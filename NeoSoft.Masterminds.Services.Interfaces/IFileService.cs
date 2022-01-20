using Microsoft.AspNetCore.Http;
using NeoSoft.Masterminds.Domain.Models.Models;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Services.Interfaces
{
    public interface IFileService
    {
        Task<ImageFileModel> DownloadFileFromFileSystem(int fileId, string basePath);

        Task<int> ConvertToUploadImageFileModel(IFormFile file, byte[] fileBytes, string basePath);
    }
}
