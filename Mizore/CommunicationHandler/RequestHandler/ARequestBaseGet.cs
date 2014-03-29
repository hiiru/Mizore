using System;
using System.Collections.Generic;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public abstract class ARequestBaseGet : IRequest
    {
        protected ARequestBaseGet(SolrUriBuilder baseBuilder)
        {
            if (baseBuilder == null) throw new ArgumentNullException("baseBuilder");
            if (baseBuilder.IsBaseUrl) throw new ArgumentException("Requests don't accept base urls", "baseBuilder");
            UrlBuilder = baseBuilder;
            Header = new Dictionary<string, string>();
        }

        public RequestMethod Method { get { return RequestMethod.GET; } }

        public INamedList Content { get { return null; } }

        public Dictionary<string, string> Header { get; private set; }

        public abstract IResponse GetResponse(INamedList nl);

        public SolrUriBuilder UrlBuilder { get; protected set; }
    }
}