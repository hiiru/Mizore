using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class CoresRequest : ARequestBaseGet
    {
        public CoresRequest(SolrUriBuilder builder)
            : base(builder)
        {
            UrlBuilder.Core = "admin";
            UrlBuilder.Handler = "cores";
        }

        public override IResponse GetResponse(INamedList nl)
        {
            return new CoresResponse(this, nl);
        }
    }
}