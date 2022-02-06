using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Filters
{
    public class MentorSearchFilter : GetFilter
    {
        public MentorOrderBy MentorOrderBy { get; set; }
    }
    public enum MentorOrderBy
    {
        Id = 1,
        FirstName,
        LastName,
    }
}
