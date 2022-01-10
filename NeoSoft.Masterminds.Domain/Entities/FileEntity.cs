using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class FileEntity : BaseEntity
    {
        public string Name { get; set; }
        public string InitialName { get; set; }
        public string ContentType { get; set; }
        public string Extention { get; set; }
        public enum FileType {get; set; }

    }
}
