using NeoSoft.Masterminds.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Filters
{
    public class ProfessionalAspectSearchFilter : GetFilter
    {
        public AspectOrderBy AspectOrderBy { get; set; }
    }
}
