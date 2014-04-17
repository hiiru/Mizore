using Mizore.ContentSerializer;
using System;

namespace Mizore.Exceptions
{
    public class MizoreSerializationException : MizoreException
    {
        public MizoreSerializationException(string message, IContentSerializer serializer, Exception innerException = null)
            : base(message, innerException)
        {
            Serializer = serializer;
        }

        public IContentSerializer Serializer { get; protected set; }
    }
}