using Mizore.CommunicationHandler.RequestHandler;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler
{
    public interface IRequestFactory
    {
        IRequest CreateRequest(string requestType, ISolrServerHandler server, string core, params object[] objects);

        IRequest CreateRequest(string requestType, ISolrServerHandler server, params object[] objects);
    }
}