using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using System;

namespace Mizore.Exceptions
{
    public class MizoreCommunicationException : MizoreException
    {
        public MizoreCommunicationException(IRequest request, Exception innerException)
            : this(request, null, innerException.Message, innerException)
        { }

        public MizoreCommunicationException(IResponse response, Exception innerException)
            : this(null, response, innerException.Message, innerException)
        { }

        public MizoreCommunicationException(IRequest request, IResponse response, string message,
            Exception innerException)
            : base(message, innerException)
        {
            Request = request;
            Response = response;
        }

        public IRequest Request { get; protected set; }

        public IResponse Response { get; protected set; }
    }
}