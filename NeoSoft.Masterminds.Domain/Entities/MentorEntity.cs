using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class MentorEntity : BaseEntity
    {
        public string Specialty { get; set; }
        public decimal HourlyRate { get; set; }
        public  string ProfessionalAspects { get; set; }
        public string? Description { get; set; }
        public ProfileEntity Profile { get; set; }
        public virtual IList<ReviewEntity> Reviews { get; set; }
    }
}
