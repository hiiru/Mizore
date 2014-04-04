using System;

namespace Mizore.Exceptions
{
    public class MizoreServerException : MizoreException
    {
        public MizoreServerException(string message, Exception innerException = null)
            : base(message, innerException)
        { }
    }
}