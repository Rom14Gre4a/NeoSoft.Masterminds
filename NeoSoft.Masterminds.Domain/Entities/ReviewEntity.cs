using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class ReviewEntity : BaseEntity
    {
        public int FromMentorId { get; set; }
        public MentorEntity Mentor { get; set; }
       
        public double Rating { get; set; }
        public string Text { get; set; }

        public ProfileEntity Owner { get; set; } 
       // public int OwnerId { get; set; }
    }
}
