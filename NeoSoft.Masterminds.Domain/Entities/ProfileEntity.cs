using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class ProfileEntity : BaseEntity
    {
        public int? PhotoId { get; set; }
        public FileEntity Photo { get; set; }
        public string ProfileFirstName { get; set; }
        public int ProfileLastName { get; set; }
        public IList<ReviewEntity> SentReivews { get; set; }

        public IList<ReviewEntity> ReceivedReivews { get; set; }

    }
}
