using Microsoft.AspNetCore.Http;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Enums;
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

        private async Task<int> UploadImageToFileSystem(UploadImageFileModel imageFile)
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

                await _fileRepository.SaveAsync(file);
                var imageId = await _fileRepository.GetAsync(fileName);
                fileId = imageId.Id;
            }
            return fileId;
        }

        private static string GetFilePath(string basePath, string fileName, string fileExtension)
        {
            var filePath = Path.Combine(basePath, FileFolderName, $"{fileName}.{fileExtension}");

            return filePath;
        }

        public async Task<ImageFileModel> DownloadFileFromFileSystem(int fileId, string basePath)
        {
            var fileMetadata = await _fileRepository.GetAsync(fileId);
            if (fileMetadata == null)
                return null;

            var filePath = GetFilePath(basePath, fileMetadata.Name, fileMetadata.Extension);

            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] file = new byte[fileStream.Length];
            await fileStream.ReadAsync(file);

            return new ImageFileModel
            {
                File = file,
                Name = fileMetadata.Name,
                InitialName = fileMetadata.InitialName,
                ContentType = fileMetadata.ContentType,
                Extension = fileMetadata.Extension,
                FileType = fileMetadata.FileType
            };
        }

        public async Task<int> ConvertToUploadImageFileModel(IFormFile file, byte[] fileBytes, string basePath)
        {
            var initialName = file.FileName.Split(".")[0];
            var extension = file.FileName.Split(".")[1];
            var fileId = await UploadImageToFileSystem(new UploadImageFileModel
            {
                File = fileBytes,
                BasePath = basePath,
                InitialName = initialName,
                ContentType = file.ContentType,
                Extension = extension,
                FileType = FileType.ProfilePhoto,
            });
            return fileId;
        }
    }
}
