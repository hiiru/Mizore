using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler
{
    public interface IRequestFactory
    {
        IRequest CreateRequest(string requestType, SolrUriBuilder builder, INamedList content = null, IQueryBuilder queryBuilder = null);
    }
}