using System;

namespace Store.Shared.Core.CustomException
{
    public class DomainException : Exception
    {
        public DomainException()
        { }

        public DomainException(string message) : base(message)
        { }

        public DomainException(string message, Exception innerExcepption) : base(message, innerExcepption)
        { }
    }
}
