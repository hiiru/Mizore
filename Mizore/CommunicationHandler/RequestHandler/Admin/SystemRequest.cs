using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class SystemRequest : ARequestBaseGet
    {
        public SystemRequest(ISolrServerHandler server)
            : base(server, "admin", "system")
        { }
    }
}