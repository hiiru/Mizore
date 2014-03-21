using System;
using System.Collections.Generic;
using System.Linq;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ConnectionHandler;
using Mizore.ContentSerializer;

namespace Mizore.SolrServerHandler
{
    public class HttpSolrServer : ISolrServerHandler
    {
        protected HttpWebRequestHandler RequestHandler;

        public bool IsReady { get; protected set; }
        public bool IsOnline { get; protected set; }
        private DateTime LastCheck;
        //TODO: define interval?
        private TimeSpan RechckInterval = new TimeSpan(0, 1, 0);

        public HttpSolrServer(string url, IContentSerializer contentSerializer = null, ICacheHandler cacheHandler = null, IRequestFactory factory = null)
        {
            //Argument Validation
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException("url");
            if (url.EndsWith("/")) url = url.TrimEnd('/');
            Uri solrUri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out solrUri)) throw new ArgumentException("the URL is invalid", "url");
            RequestHandler=new HttpWebRequestHandler();
            if (!RequestHandler.IsUriSupported(solrUri)) throw new ArgumentException("the URL is invalid", "url");

            //Initialization
            ServerAddress = url;
            Serializer = contentSerializer ?? new EasynetJavabinSerializer();
            Cache = cacheHandler ?? null;
            RequestFactory = factory ?? new RequestFactory();
            IsReady = true;
            CheckStatus(true);
        }

        private bool CheckStatus(bool loadCores=false)
        {
            if (LastCheck - DateTime.Now > RechckInterval) return IsOnline;
            LastCheck = DateTime.Now;
            PingResponse ping;
            IsOnline = TryRequest(RequestFactory.CreateRequest("ping", this), out ping) &&
                        ping.Status.Equals("OK", StringComparison.InvariantCultureIgnoreCase);

            if (IsOnline && loadCores)
            {
                CoresResponse coresResponse;
                if (TryRequest(RequestFactory.CreateRequest("cores", this), out coresResponse))
                {
                    Cores = coresResponse.Cores.Select(x => x.Name).ToList();
                    DefaultCore = coresResponse.DefaultCore;
                }
            }
            return IsOnline;
        }

        public List<string> Cores { get; protected set; }

        public string DefaultCore { get; set; }
 
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

        public bool TryRequest<T>(IRequest request, out T response, string core = null) where T : class, IResponse
        {
            response = null;
            try
            {
                response = RequestHandler.Request<T>(request);
            }
            catch
            {
                return false;
            }
            return response != null;
        }

        //public IResponse Request(object obj, IRequest request, string core = null)
        //{
        //    throw new NotImplementedException();
        //}

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