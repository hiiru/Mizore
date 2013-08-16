using Mizore.CommunicationHandler.RequestHandler;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler
{
    public class RequestFactory : IRequestFactory
    {
        public IRequest CreateRequest(string requestType, ISolrServerHandler server, string core, params object[] objects)
        {
            switch (requestType.ToLower())
            {
                case "ping":
                    return new PingRequest(server);

                default:
                    return null;
            }
        }

        public IRequest CreateRequest(string requestType, ISolrServerHandler server, params object[] objects)
        {
            return CreateRequest(requestType, server, server.DefaultCore, objects);
        }
    }
}