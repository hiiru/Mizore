using System;
using System.Collections.Specialized;
using Mizore.SolrServerHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class UpdateRequest : IRequest
    {
        public const string Handler = "/admin/system";

        //TODO: how is the Data passed to the Request?
        public UpdateRequest(ISolrServerHandler server,string core)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (core == null) throw new ArgumentNullException("core");
            Server = server;
            Core = core;

            var url = new UriBuilder(server.ServerAddress + Handler);
            if (server.Serializer != null)
                url.Query = "wt=" + server.Serializer.wt;
            Url = url.Uri;
        }

        public virtual string Method { get { return "POST"; } }

        public Uri Url { get; protected set; }

        public ISolrServerHandler Server { get; protected set; }

        public string Core { get; protected set; }

        public virtual INamedList Content { get { return null; } }

        public virtual NameValueCollection Header { get { return null; } }

        public virtual string CacheKey { get { return null; } }
    }
}