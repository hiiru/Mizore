using System;
using System.Collections.Specialized;
using System.IO;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class QueryRequest : Request
    {
        public QueryRequest(ISolrServerHandler server, Stream data = null, string core = null)
            : base(server, data, core)
        {
        }

        public override string Method
        {
            get { throw new NotImplementedException(); }
        }

        public override Uri Url
        {
            get { throw new NotImplementedException(); }
        }

        public override Stream Content
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override NameValueCollection Header
        {
            get { throw new NotImplementedException(); }
        }

        public override string CacheKey
        {
            get { throw new NotImplementedException(); }
        }
    }
}