using Mizore.DataMappingHandler;
using System;

namespace Mizore.Exceptions
{
    public class MizoreMappingException : MizoreException
    {
        public MizoreMappingException(IDataMappingHandler handler, Exception innerException)
            : this(handler, innerException.Message, innerException)
        {
        }

        public MizoreMappingException(IDataMappingHandler handler, string message, Exception innerException = null)
            : base(message, innerException)
        {
            DataMappingHandler = handler;
        }

        public IDataMappingHandler DataMappingHandler { get; protected set; }
    }
}