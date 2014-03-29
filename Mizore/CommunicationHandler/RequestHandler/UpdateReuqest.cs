using System;
using System.Collections.Generic;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer.Data;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class UpdateRequest : IRequest
    {
        public const string Handler = "/admin/system";

        //TODO: how is the Data passed to the Request?
        public UpdateRequest(ISolrServerHandler server, string core)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (core == null) throw new ArgumentNullException("core");
            Server = server;
            Core = core;

            throw new NotImplementedException("TODO: Update Request");
        }

        public virtual RequestMethod Method { get { return RequestMethod.POST; } }

        public SolrUriBuilder UrlBuilder { get; private set; }

        public Uri Url { get; protected set; }

        public ISolrServerHandler Server { get; protected set; }

        public string Core { get; protected set; }

        public virtual INamedList Content { get { return null; } }

        public Dictionary<string, string> Header { get; protected set; }

        public IResponse GetResponse(INamedList nl)
        {
            throw new NotImplementedException();
        }

        public virtual string CacheKey { get { return null; } }
    }
}