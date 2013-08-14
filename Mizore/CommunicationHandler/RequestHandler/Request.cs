using System;
using System.Collections.Specialized;
using System.IO;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public abstract class Request
    {
        protected ISolrServerHandler _server;
        protected Stream _content;
        protected string _core;

        //TODO-HIGH: how to pass data to the constructor?
        protected Request(ISolrServerHandler server, Stream data = null, string core = null)
        {
            _server = server;
            _content = data;
            _core = core;
        }

        /// <summary>
        /// Http Method
        /// </summary>
        public virtual string Method { get { return "GET"; } }

        /// <summary>
        /// Whole url + handler
        /// TODO-HIGH: Where to handle the querystring, request or connection?
        /// </summary>
        public abstract Uri Url { get; }

        public virtual ISolrServerHandler Server { get { return _server; } }

        public virtual string Core { get { return _core ?? _server.DefaultCore; } }

        /// <summary>
        /// Prepared Stream for transmission, can be null.
        /// </summary>
        public virtual Stream Content { get { return _content; } set { _content = value; } }

        //Optional: additional required headers ?
        public virtual NameValueCollection Header { get { return null; } }

        /// <summary>
        /// Returns CacheKey for this request
        /// </summary>
        public abstract string CacheKey { get; }
    }
}