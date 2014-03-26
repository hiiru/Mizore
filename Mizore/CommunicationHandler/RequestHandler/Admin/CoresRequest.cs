using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class CoresRequest : ARequestBaseGet
    {
        public CoresRequest(ISolrServerHandler server)
            : base(server, "admin", "cores")
        { }
    }
}