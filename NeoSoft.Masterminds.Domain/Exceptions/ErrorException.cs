using System;

namespace NeoSoft.Masterminds.Domain.Models.Exceptions
{
    //500
    public class ErrorException : Exception
    {
        public ErrorException()
            : base("Internal Server Error")
        {

        }

    }
}
