

using Microsoft.AspNetCore.Http;

namespace NeoSoft.Masterminds.Models.Incoming
{
    public class UploadProfilePhoto
    {
        public IFormFile Avatar { get; set; }
    }
}
