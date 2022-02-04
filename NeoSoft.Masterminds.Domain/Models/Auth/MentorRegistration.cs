using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Models.Auth
{
    public class MentorRegistration : Registration
    {
        public List<string> Professions { get; set; }

        public List<string> ProfessionalAspects { get; set; }

        public string Description { get; set; }

        public int HourlyRate { get; set; }
    }
}
