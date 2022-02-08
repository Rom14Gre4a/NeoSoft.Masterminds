using NeoSoft.Masterminds.Domain.Models.Filters;

namespace NeoSoft.Masterminds.Models.Incoming.Filters
{
    public class MentorFilterApiModel : FilterBaseApiModel
    
    {
        public MentorOrderBy MentorOrderBy { get; set; }
    }
}
