using NeoSoft.Masterminds.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Models
{
    public class UploadImageToFileModel
    {
        public byte[] File { get; set; }

        public string BasePath { get; set; }

        public string InitialName { get; set; }

        public string ContentType { get; set; }

        public string Extension { get; set; }

        public FileType FileType { get; set; }
    }
}
