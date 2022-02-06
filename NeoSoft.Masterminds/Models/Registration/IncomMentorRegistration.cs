using System.Collections.Generic;

namespace NeoSoft.Masterminds.Models.Registration
{
    public class IncomMentorRegistration : IncomRegistration
    {
        public decimal HourlyRate { get; set; }
       
        public List<string> Professions { get; set; }

        public List<string> ProfessionalAspects { get; set; }
        //public IList<ProfessionViewModel> Professions { get; set; } 
    }
}
