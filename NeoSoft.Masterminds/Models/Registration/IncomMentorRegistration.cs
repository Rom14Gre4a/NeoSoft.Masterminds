using System.Collections.Generic;

namespace NeoSoft.Masterminds.Models.Registration
{
    public class IncomMentorRegistration : IncomRegistration
    {
        public decimal HourlyRate { get; set; }
        public IList<ProfessionViewModel> Professions { get; set; } 
    }
}
