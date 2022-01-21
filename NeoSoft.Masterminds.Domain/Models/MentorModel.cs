using NeoSoft.Masterminds.Domain.Models.Models;
using System;
using System.Collections.Generic;

namespace NeoSoft.Masterminds.Domain
{
    public class MentorModel
    {
        public int Id { get; set; }
        public int ProfilePhotoId { get; set; }
        public string FirstName { get; set; }    
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public double Rating { get; set; }
        public int ReviewsTotalCount { get; set; }
        public decimal HourlyRate { get; set; }
        public List<string> ProfessionalAspects { get; set; }
        public string Description { get; set; }
        public List<ReviewModel> Reviews { get; set; } 
    }
}
