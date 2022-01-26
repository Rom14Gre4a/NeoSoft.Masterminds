using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class MentorEntity : BaseEntity
    {
        public string Specialty { get; set; }
        public decimal HourlyRate { get; set; }
        public  string ProfessionalAspects { get; set; }
        public string Description { get; set; }
        public ProfessionEntity Prof { get; set; }
        public ProfileEntity Profile { get; set; }
        public virtual IList<ReviewEntity> Reviews { get; set; }
        public List<ProfessionEntity> Professions { get; set; }
    }
}
