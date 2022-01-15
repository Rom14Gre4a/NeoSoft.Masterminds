using Microsoft.AspNetCore.Mvc;
using NeoSoft.Masterminds.Domain.Models.Enums;

namespace NeoSoft.Masterminds.Models


{
    public class GetListItems
    {
        [BindProperty(Name = "skip")]
        public int? Skip { get; set; }

        [BindProperty(Name = "take")]
        public int? Take { get; set; }

        [BindProperty(Name = "searchText")]
        public string SearchText { get; set; }

        [BindProperty(Name = "orderByProperty")]
        public string OrderByProperty { get; set; }

        [BindProperty(Name = "sortOrder")]
        public SortOrder? SortOrder { get; set; }
    }
}
