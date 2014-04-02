using System;
using System.Collections.Generic;
using System.Linq;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.RequestHandler.Admin;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ConnectionHandler;
using Mizore.ContentSerializer;
using Mizore.DataMappingHandler;
using Mizore.Exceptions;

namespace Mizore.SolrServerHandler
{
    public class HttpSolrServer : ISolrServerHandler
    {
        #region Properties

        public List<string> Cores { get; protected set; }

        public string DefaultCore { get; set; }

        public ICacheHandler Cache { get; protected set; }

        public IContentSerializerFactory SerializerFactory { get; protected set; }

        public IDataMappingHandler DataMapping { get; protected set; }

        public bool IsReady { get; protected set; }

        protected bool _isOnline;

        public bool IsOnline { get { return _isOnline || CheckStatus(); } }

        public TimeSpan RecheckInterval { get; set; }

        public DateTime LastCheck { get; protected set; }

        #endregion Properties

        private readonly SolrUriBuilder baseUriBuilder;
        protected HttpWebRequestHandler RequestHandler;

        public HttpSolrServer(string url, IContentSerializerFactory contentSerializerFactory = null, ICacheHandler cacheHandler = null)
        {
            //Argument Validation
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException("url");
            if (url.EndsWith("/")) url = url.TrimEnd('/');
            Uri solrUri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out solrUri)) throw new ArgumentException("the URL is invalid", "url");
            RequestHandler = new HttpWebRequestHandler();
            if (!RequestHandler.IsUriSupported(solrUri)) throw new ArgumentException("the URL is invalid", "url");

            //Initialization
            RecheckInterval = new TimeSpan(0, 1, 0);
            baseUriBuilder = new SolrUriBuilder(solrUri);
            SerializerFactory = contentSerializerFactory ?? new ContentSerializerFactory();
            DataMapping = new DataMappingCollection();
            Cache = cacheHandler ?? null;
            IsReady = true;
            CheckStatus(true);
        }

        private bool CheckStatus(bool loadCores = false)
        {
            if (_isOnline) return true;
            if (LastCheck - DateTime.Now > RecheckInterval) return false;
            LastCheck = DateTime.Now;
            try
            {
                var ping = RequestHandler.Request<PingResponse>(new PingRequest(GetUriBuilder()), SerializerFactory);
                _isOnline = ping != null && ping.Status.Equals("OK", StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                _isOnline = false;
                return false;
            }
            if (_isOnline && loadCores)
            {
                var coresResponse = RequestHandler.Request<CoresResponse>(new CoresRequest(GetUriBuilder()), SerializerFactory);
                if (coresResponse != null)
                {
                    Cores = coresResponse.Cores.Select(x => x.Name).ToList();
                    DefaultCore = coresResponse.DefaultCore;
                }
            }
            return _isOnline;
        }

        public SolrUriBuilder GetUriBuilder(string core = null, string handler = null)
        {
            return baseUriBuilder.GetBuilder(core ?? DefaultCore, handler);
        }

        public bool TryRequest<T>(IRequest request, out T response, string core = null) where T : class, IResponse
        {
            response = null;
            if (!IsOnline)
                return false;

            try
            {
                response = Request<T>(request, core);
            }
            catch
            {
                return false;
            }
            return response != null;
        }

        public T Request<T>(IRequest request, string core = null) where T : class, IResponse
        {
            if (!IsOnline) throw new MizoreException("Server offline");
            return RequestHandler.Request<T>(request, SerializerFactory);
        }
    }
}