using System.Collections.Generic;

namespace NeoSoft.Masterminds.Domain.Models.Responses
{
    public class ValidationMessage
    {
        public string Field { get; set; }
        public List<string> Messages { get; set; }
    }
}
