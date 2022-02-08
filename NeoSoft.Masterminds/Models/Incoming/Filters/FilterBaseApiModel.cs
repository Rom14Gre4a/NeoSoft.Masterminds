using NeoSoft.Masterminds.Domain.Models.Enums;

namespace NeoSoft.Masterminds.Models.Incoming.Filters
{
    public class FilterBaseApiModel
    {
        public int Skip { get; set; }

        public int Take { get; set; } = 15;

        public string OrderByProperty { get; set; }

        public string SearchText { get; set; }

        public SortOrder SortOrder { get; set; } = SortOrder.Descending;
    }
}
