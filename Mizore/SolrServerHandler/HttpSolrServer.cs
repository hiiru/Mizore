using System;
using System.Collections.Generic;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ConnectionHandler;
using Mizore.ContentSerializer;
using Mizore.Exceptions;

namespace Mizore.SolrServerHandler
{
    public class HttpSolrServer : ISolrServerHandler
    {
        protected HttpWebRequestHandler RequestHandler;

        public bool IsReady { get; protected set; }

        public HttpSolrServer(string url, IContentSerializer contentSerializer = null, ICacheHandler cacheHandler = null, IRequestFactory factory = null)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException("url");
            if (url.EndsWith("/")) url = url.TrimEnd('/');
            ServerAddress = url;
            Serializer = contentSerializer ?? new EasynetJavabinSerializer();
            Cache = cacheHandler ?? null;
            RequestFactory = factory ?? new RequestFactory();

            Uri solrUri;
            if (!Uri.TryCreate(ServerAddress, UriKind.Absolute, out solrUri)) throw new ArgumentException("the URL is invalid", "url");
            RequestHandler=new HttpWebRequestHandler();
            if (!RequestHandler.IsUriSupported(solrUri)) throw new ArgumentException("the URL is invalid", "url");


            IsReady = true;

            //TODO: Multicore Mode - Initialize related properties, (MulticoreMode,Cores,DefaultCore)
            //TODO: Timeout?
        }
        
        public List<string> Cores
        {
            get { throw new NotImplementedException(); }
        }

        //TODO: DefaultCore handling -  SOLR-5 should simplify this
        public string DefaultCore
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        public bool MulticoreMode
        {
            get { return false; }
        }
        
        public string ServerAddress { get; protected set; }

        public ICacheHandler Cache { get; protected set; }

        public IContentSerializer Serializer { get; protected set; }

        public IRequestFactory RequestFactory { get; protected set; }

        public int ConnectionTimeout
        {
            get { return 0; }
            set { throw new NotImplementedException(); }
        }

        public void Request(object obj, IRequest request, string core = null)
        {
            throw new NotImplementedException();
        }

        //TODO: how is the Data passed to the Request?
        public UpdateResponse Add(string core = null)
        {
            return RequestHandler.Request<UpdateResponse>(RequestFactory.CreateRequest("update", this, core));
        }
        
        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public bool Commit(string core = null)
        {
            throw new NotImplementedException();
        }

        public bool Optimize(string core = null)
        {
            throw new NotImplementedException();
        }

        public PingResponse Ping()
        {
            return RequestHandler.Request<PingResponse>(RequestFactory.CreateRequest("ping", this));
        }

        public SystemResponse GetSystemInfo()
        {
            return RequestHandler.Request<SystemResponse>(RequestFactory.CreateRequest("system", this));
        }

        public void GetVersion()
        {
            throw new NotImplementedException();
        }
    }
}