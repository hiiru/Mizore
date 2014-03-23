using System;
using System.Collections.Specialized;
using Mizore.SolrServerHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class SystemRequest : ARequestBaseGet
    {
        public SystemRequest(ISolrServerHandler server)
            : base(server, "admin", "system")
        {}
    }
}