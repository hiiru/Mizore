using System;
using System.Collections.Specialized;
using Mizore.SolrServerHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class QueryRequest : IRequest
    {
        public QueryRequest(ISolrServerHandler server, INamedList data, string core = null)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (data == null) throw new ArgumentNullException("data");
            Server = server;
            Core = core ?? server.DefaultCore;
        }

        public string Method { get; protected set; }

        public Uri Url { get; protected set; }

        public ISolrServerHandler Server { get; protected set; }

        public string Core { get; protected set; }

        public INamedList Content { get; protected set; }

        public NameValueCollection Header { get; protected set; }

        public string CacheKey { get; protected set; }
    }
}