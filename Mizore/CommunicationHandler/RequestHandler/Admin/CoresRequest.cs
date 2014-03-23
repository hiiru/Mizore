using System;
using System.Collections.Specialized;
using Mizore.SolrServerHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class CoresRequest : ARequestBaseGet
    {
        public CoresRequest(ISolrServerHandler server)
            : base(server, "admin", "cores")
        {}
    }
}