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
        public string Description { get; set; }
        public double Rating { get; set; }
        public IList<ProfessionalAspectEntity> ProfessionalAspects { get; set; }
        public ProfileEntity Profile { get; set; }
        public IList<ProfessionEntity> Professions { get; set; }
    }
}
