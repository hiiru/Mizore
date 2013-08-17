using Mizore.CommunicationHandler.RequestHandler;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler
{
    public class RequestFactory : IRequestFactory
    {
        public IRequest CreateRequest(string requestType, ISolrServerHandler server, string core, params object[] objects)
        {
            if (core == null) core = server.DefaultCore;
            switch (requestType.ToLower())
            {
                case "update":
                    //TODO: how is the Data passed to the Request?
                    return new UpdateRequest(server,core);
                case "ping":
                    return new PingRequest(server);
                case "system":
                    return new SystemRequest(server);
                default:
                    return null;
            }
        }

        public IRequest CreateRequest(string requestType, ISolrServerHandler server, params object[] objects)
        {
            return CreateRequest(requestType, server, null, objects);
        }
    }
}