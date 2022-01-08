using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class MentorEntity : BaseEntity
    {
        public string Specialty { get; set; }
        public decimal Rate { get; set; }
        public List<string> ProfessionalAspects { get; set; }
        public string Description { get; set; }
        public ProfileEntity Profile { get; set; }
        public virtual List<ReviewModel> Reviews { get; set; }
    }
}
