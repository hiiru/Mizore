using System;
using System.Collections.Specialized;
using Mizore.SolrServerHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class SelectRequest : ARequestBaseGet
    {
        public SelectRequest(ISolrServerHandler server, string query, string core = null)
            : base(server, core, "select")
        {
            uriBuilder.Query["q"] = query;
        }
    }
}