using System;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class PingRequest : Request
    {
        public const string Handler = "/admin/ping";

        public PingRequest(ISolrServerHandler server)
            : base(server)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (!server.AdminCore) throw new ArgumentException("PingRequest requires a Server AdminCore!", "server");
            var url = new UriBuilder(server.ServerAddress + Handler);
				if (server.Serializer!=null)
	        url.Query = "wt="+server.Serializer.wt;
	        _url = url.Uri;
        }

        protected Uri _url;

        public override Uri Url
        {
            get { return _url; }
        }

        public override string CacheKey
        {
            get { throw new NotImplementedException(); }
        }
    }
}