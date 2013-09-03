using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mizore.Exceptions
{
    /// <summary>
    /// Base Exception for all Mizore Exceptions.
    /// </summary>
    public class MizoreException : Exception
    {
        public MizoreException(string message) : base(message) { }

        public MizoreException(string message, Exception innerException) : base(message, innerException) { }
    }
}
