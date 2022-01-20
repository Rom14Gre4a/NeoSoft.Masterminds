using Microsoft.AspNetCore.Http;
using System.IO;

namespace NeoSoft.Masterminds.Common.Extensions
{
    public static class HttpRequestExtension
    {
        public static byte [] GetFileToByte(IFormFile file)
        {
            using var fileStream = new MemoryStream();
            file.CopyTo(fileStream);
            var fileBytes = fileStream.ToArray();
            return fileBytes;

        }
    }

   
       
}
