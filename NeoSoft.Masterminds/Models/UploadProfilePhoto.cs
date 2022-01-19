

using Microsoft.AspNetCore.Http;

namespace NeoSoft.Masterminds.Models
{
    public class UploadProfilePhoto 
    {
        public IFormFile Avatar { get; set; }
    }
}
