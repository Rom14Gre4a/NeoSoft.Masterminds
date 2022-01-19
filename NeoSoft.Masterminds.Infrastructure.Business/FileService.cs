using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class FileService : IFileService
    {
        private const string FileFolderName = "Files";
        private readonly IFileRepository _fileRepository;
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<int> UploadImageToFileSystem(UploadImageToFileModel imageFile)
        {
            var fileId = 0;
            if (imageFile != null)
            {
                var fileName = Guid.NewGuid().ToString();
                var filePath = GetFilePath(imageFile.BasePath, fileName, imageFile.Extension);

                await File.WriteAllBytesAsync(filePath, imageFile.File);

                var file = new FileEntity
                {
                    Name = fileName,
                    InitialName = imageFile.InitialName,
                    ContentType = imageFile.ContentType,
                    Extension = imageFile.Extension,
                    FileType = imageFile.FileType
                };

                await _fileRepository.Save(file);
            }
            return fileId;
        }

        private static string GetFilePath(string basePath, string fileName, string fileExtension)
        {
            var filePath = Path.Combine(basePath, FileFolderName, $"{fileName}.{fileExtension}");

            return filePath;
        }
    }
}
