using NeoSoft.Masterminds.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models
{
    public class GetFilter
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public string SearchText { get; set; }

        public string OrderByProperty { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}
