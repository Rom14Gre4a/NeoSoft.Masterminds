using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Models
{
    public class MentorListModel
    {
        public int Id { get; set; }
        public int ProfilePhotoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Rating { get; set; }
        public string Specialty { get; set; }
        public decimal HourlyRate { get; set; }
        public string Description { get; set; }

    }

}
