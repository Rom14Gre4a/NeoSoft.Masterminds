using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Models
{
   public class ReviewModel
    {
        public int Id { get; set; }

        public int ProfilePhotoId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Rating { get; set; }

        public string Text { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
