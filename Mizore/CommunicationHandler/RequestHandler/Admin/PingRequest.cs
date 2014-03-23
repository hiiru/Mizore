using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class PingRequest : ARequestBaseGet
    {
        public PingRequest(ISolrServerHandler server)
            : base(server, "admin", "ping")
        {}
    }
}