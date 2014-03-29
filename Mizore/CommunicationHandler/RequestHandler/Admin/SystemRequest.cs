using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class SystemRequest : ARequestBaseGet
    {
        public SystemRequest(SolrUriBuilder builder)
            : base(builder)
        {
            UrlBuilder.Core = "admin";
            UrlBuilder.Handler = "system";
        }

        public override IResponse GetResponse(INamedList nl)
        {
            return new SystemResponse(this, nl);
        }
    }
}