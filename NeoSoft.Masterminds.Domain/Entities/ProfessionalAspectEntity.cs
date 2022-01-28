using System.Collections.Generic;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class ProfessionalAspectEntity : BaseEntity
    {
        public string Aspect { get; set; }
        public IList<MentorEntity> Mentors { get; set; }
    }
}
