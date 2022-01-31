using System;

namespace NeoSoft.Masterminds.Domain.Models.Exceptions
{
    // 403
    public class ForbiddenException : Exception
    {
        public ForbiddenException()
           : base("Access is denied. You are not authorized.")
        {

        }
    }
}
