using NeoSoft.Masterminds.Domain.Models.Enums;

namespace NeoSoft.Masterminds.Models.Incoming.Filters
{
    public class ProfAspFilterApiModel : FilterBaseApiModel
    {
        public AspectOrderBy AspectOrderBy { get; set; }
    }
}
