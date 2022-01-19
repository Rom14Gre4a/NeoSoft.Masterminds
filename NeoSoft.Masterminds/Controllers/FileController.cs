using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NeoSoft.Masterminds.Domain.Models.Enums;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
        [Route("api/file")]
        [ApiController]
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IFileService _fileService;
        public FileController(IWebHostEnvironment appEnvironment, IFileService fileService)
        {
            _appEnvironment = appEnvironment;
            _fileService = fileService;
        }

        [HttpPost("upload-profile-photo")]
        public async Task<ActionResult<int>> UploadProfilePhoto([FromForm] UploadProfilePhoto uploadProfilePhoto)
        {
          var fileId = 0;

           try
           {
                if (uploadProfilePhoto.Avatar.Length > 0)
                {
                    using var fileStream = new MemoryStream();
                    uploadProfilePhoto.Avatar.CopyTo(fileStream);
                    var fileBytes = fileStream.ToArray();

                    var initialName = uploadProfilePhoto.Avatar.FileName.Split(".")[0];
                    var extension = uploadProfilePhoto.Avatar.FileName.Split(".")[1];

                    fileId = await _fileService.UploadImageToFileSystem(new UploadImageToFileModel
                    {
                        File = fileBytes,
                        BasePath = _appEnvironment.WebRootPath,
                        InitialName = initialName,
                        ContentType = uploadProfilePhoto.Avatar.ContentType,
                        Extension = extension,
                        FileType = FileType.ProfilePhoto,
                    });
                }
                return fileId;

            }
            catch (Exception ex)
            {
            return fileId;
           }
        }

    }
}
