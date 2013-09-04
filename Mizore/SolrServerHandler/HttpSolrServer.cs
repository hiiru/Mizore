using System;
using System.Collections.Generic;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ConnectionHandler;
using Mizore.ContentSerializer;

namespace Mizore.SolrServerHandler
{
    public class HttpSolrServer : ISolrServerHandler
    {
        public HttpSolrServer(string url, IContentSerializer contentSerializer = null, ICacheHandler cacheHandler = null, IRequestFactory factory = null)
        {
            //TODO-HIGH: SOLR-17: url validation -> is it a valid solr url? what to do if not?
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(url);
            if (!url.Substring(0,4).Equals("http", StringComparison.InvariantCultureIgnoreCase)) throw new ArgumentException("The solr url has to be a HTTP(S):// url!", "url");
            if (url.Contains("?")) throw new ArgumentException("Invalid solr url, The solr URL must not contain parameters: "+ url,"url");
            if (url.EndsWith("/")) url = url.TrimEnd('/');
            ServerAddress = url;
            
            Serializer = contentSerializer ?? new EasynetJavabinSerializer();
            Cache = cacheHandler ?? null;
            RequestFactory = factory ?? new RequestFactory();


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
            var handler = new HttpWebRequestHandler();
            return handler.Request<UpdateResponse>(RequestFactory.CreateRequest("update", this, core));
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
            var handler = new HttpWebRequestHandler();
            return handler.Request<PingResponse>(RequestFactory.CreateRequest("ping", this));
        }

        public SystemResponse GetSystemInfo()
        {
            var handler = new HttpWebRequestHandler();
            return handler.Request<SystemResponse>(RequestFactory.CreateRequest("system", this));
        }

        public void GetVersion()
        {
            throw new NotImplementedException();
        }
    }
}