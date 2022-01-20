using NeoSoft.Masterminds.Domain.Models.Enums;

namespace NeoSoft.Masterminds.Domain.Models.Models
{
    public class UploadImageFileModel
    {
        public byte[] File { get; set; }

        public string BasePath { get; set; }

        public string InitialName { get; set; }

        public string ContentType { get; set; }

        public string Extension { get; set; }

        public FileType FileType { get; set; }
    }
}
