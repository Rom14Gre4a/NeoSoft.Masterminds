using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Common.Extensions;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Models.Incoming;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger, IWebHostEnvironment appEnvironment, IFileService fileService)
        {
            _appEnvironment = appEnvironment;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpPost("upload-profile-photo")]
        public async Task<ApiResponse<int>> UploadProfilePhoto([FromForm] UploadProfilePhoto uploadProfilePhoto)
        {
            _logger.LogInformation("Post photo action started");

            var fileId = 0;

            try
            {
                if (uploadProfilePhoto.Avatar.Length > 0)
                {
                    var fileBytes = HttpRequestExtension.GetFileToByte(uploadProfilePhoto.Avatar);
                    fileId = await _fileService.ConvertToUploadImageFileModel(uploadProfilePhoto.Avatar, fileBytes, _appEnvironment.WebRootPath);
                }
                return fileId;

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Post photo action finished successfuly");

                return fileId;
            }
        }

        [HttpGet("{fileId:int}")]
        public async Task<ApiResponse<ActionResult>> GetFile(int fileId)
        {
            _logger.LogInformation("Get photo action started");

            try
            {
                var imageFile = await _fileService.DownloadFileFromFileSystem(fileId, _appEnvironment.WebRootPath);
                return File(imageFile.File, imageFile.ContentType, $"{imageFile.Name}.{imageFile.Extension}");
            }
            catch (NotFoundException)
            {
                return NotFound($"File with id '{fileId}' not found");
            }
            catch (ErrorException)
            {
                _logger.LogInformation("Get photo action finished successfuly");

                return StatusCode(500, "Internal server error");
            }
        }

    }
}
