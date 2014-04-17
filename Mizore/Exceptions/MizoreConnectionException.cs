using Mizore.CommunicationHandler.RequestHandler;
using System;

namespace Mizore.Exceptions
{
    public class MizoreConnectionException : MizoreException
    {
        public MizoreConnectionException(IRequest request, Exception innerException)
            : this(request, innerException.Message, innerException)
        { }

        public MizoreConnectionException(IRequest request, string message, Exception innerException)
            : base(message, innerException)
        {
            Request = request;
        }

        public IRequest Request { get; protected set; }
    }
}