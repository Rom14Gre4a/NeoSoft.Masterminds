using NeoSoft.Masterminds.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class ProfessionEntity : BaseEntity
    {
       public String Name { get; set; }
        public List<MentorEntity> Mentors { get; set; }
    }
}
