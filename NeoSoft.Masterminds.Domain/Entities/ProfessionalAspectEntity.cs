using System.Collections.Generic;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class ProfessionalAspectEntity 
    {
        public int Id { get; set; }
        public string Aspect { get; set; }
        public IList<MentorEntity> Mentors { get; set; }
    }
}
