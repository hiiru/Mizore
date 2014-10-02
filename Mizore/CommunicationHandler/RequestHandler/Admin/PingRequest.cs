using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class PingRequest : ARequestBaseGet
    {
        public PingRequest(SolrUriBuilder builder)
            : base(builder)
        {
            UrlBuilder.Handler = "admin/ping";
        }

        public override IResponse GetResponse(INamedList nl)
        {
            return new PingResponse(this, nl);
        }
    }
}