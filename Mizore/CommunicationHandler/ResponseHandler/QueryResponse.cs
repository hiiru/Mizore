using System.IO;
using Mizore.CommunicationHandler.RequestHandler;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class QueryResponse : IResponse
    {
        public void Parse(IRequest request, Stream content)
        {
            Request = request;
        }

        public IRequest Request { get; protected set; }
    }
}