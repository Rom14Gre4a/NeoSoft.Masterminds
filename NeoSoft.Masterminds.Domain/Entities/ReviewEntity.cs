using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class ReviewEntity : BaseEntity
    {
        public int FromProfileId { get; set; }
        public ProfileEntity FromProfile { get; set; }

        public int ToProfileId { get; set; }
        public ProfileEntity ToProfile { get; set; } 

        public string Text { get; set; }
        public double Rating { get; set; }
    }
}
