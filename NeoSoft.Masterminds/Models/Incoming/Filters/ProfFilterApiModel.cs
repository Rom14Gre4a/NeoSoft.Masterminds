using NeoSoft.Masterminds.Domain.Models.Enums;

namespace NeoSoft.Masterminds.Models.Incoming.Filters
{
    public class ProfFilterApiModel : FilterBaseApiModel
    {
        public NameOrderBy NameOrderBy { get; set; }
    }
}
