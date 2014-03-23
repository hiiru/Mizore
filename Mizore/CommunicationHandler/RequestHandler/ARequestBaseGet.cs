using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Mizore.SolrServerHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public abstract class ARequestBaseGet : IRequest
    {
        protected ARequestBaseGet(ISolrServerHandler server, string core, string handler)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (core==null) core= server.DefaultCore;
            Server = server;
            uriBuilder = server.SolrUriBuilder.GetBuilder(core, handler);
            if (server.Serializer != null)
                uriBuilder.Query["wt"]=server.Serializer.wt;
        }
        
        public string Method { get { return "GET"; } }
        public INamedList Content { get { return null; } }

        protected SolrUriBuilder uriBuilder;


        public Uri Url { get { return  uriBuilder.Uri; } }
        public ISolrServerHandler Server { get; protected set; }

        //For future use
        public virtual NameValueCollection Header { get { return null; } }
        public string CacheKey { get { return null; } }
    }
}
