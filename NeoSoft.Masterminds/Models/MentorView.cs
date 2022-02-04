using System.Collections.Generic;

namespace NeoSoft.Masterminds.Models
{
    public class MentorView
    {
        public int Id { get; set; }

        public string ProfilePhoto { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Professions { get; set; }

        public double Rating { get; set; }

        public int ReviewsTotalCount { get; set; }

        public decimal HourlyRate { get; set; }

        public List<string> ProfessionalAspects { get; set; }

        public string Description { get; set; }

        public List<ReviewView> Reviews { get; set; }
    }
}

