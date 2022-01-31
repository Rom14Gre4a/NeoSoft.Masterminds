using System;

namespace NeoSoft.Masterminds.Domain.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        //404
        public NotFoundException(string message)
            : base(message)
        {

        }

        public NotFoundException()
            : base("Not found")
        {

        }
    }
}
