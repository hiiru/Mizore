using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class GetRequest : ARequestBaseGet
    {
        public GetRequest(SolrUriBuilder builder, string id = null)
            //ISolrServerHandler server, IQueryBuilder queryBuilder, string core = null)
            : base(builder)
        {
            UrlBuilder.Handler = "get";
            if (id != null)
                UrlBuilder.Query["id"] = id;
        }

        public override IResponse GetResponse(INamedList nl)
        {
            return new GetResponse(this, nl);
        }
    }
}