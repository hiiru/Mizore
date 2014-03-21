using System;
using System.Collections.Specialized;
using Mizore.SolrServerHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class PingRequest : IRequest
    {
        public const string Handler = "/admin/ping";

        public PingRequest(ISolrServerHandler server)
        {
            if (server == null) throw new ArgumentNullException("server");
            Server = server;

            var url = new UriBuilder(server.ServerAddress + Handler);
            if (server.Serializer != null)
                url.Query = "wt=" + server.Serializer.wt;
            Url = url.Uri;
        }

        public virtual string Method { get { return "GET"; } }

        public Uri Url { get; protected set; }

        public ISolrServerHandler Server { get; protected set; }

        public virtual string Core { get { return "admin"; } }

        public virtual INamedList Content { get { return null; } }

        public virtual NameValueCollection Header { get { return null; } }

        public virtual string CacheKey { get { return null; } }
    }
}