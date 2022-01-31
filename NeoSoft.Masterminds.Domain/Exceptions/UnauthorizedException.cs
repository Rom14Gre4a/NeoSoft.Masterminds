using System;

namespace NeoSoft.Masterminds.Domain.Models.Exceptions
{
    // 401
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base("Not authorized")
        {

        }
    }
}
