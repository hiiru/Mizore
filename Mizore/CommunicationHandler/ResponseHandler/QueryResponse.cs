using System.IO;
using Mizore.CommunicationHandler.RequestHandler;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class QueryResponse : IResponse
    {
        public void Parse(Request request, Stream content)
        {
            Request = request;
        }

        public Request Request { get; protected set; }
    }
}