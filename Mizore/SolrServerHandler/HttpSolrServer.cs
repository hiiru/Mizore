﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ConnectionHandler;
using Mizore.ContentSerializer;
using Mizore.Exceptions;

namespace Mizore.SolrServerHandler
{
    public class HttpSolrServer : ISolrServerHandler
    {
        #region Properties
        public List<string> Cores { get; protected set; }

        public string DefaultCore { get; set; }

        public string ServerAddress { get; protected set; }

        public ICacheHandler Cache { get; protected set; }

        public IContentSerializer Serializer { get; protected set; }

        public IRequestFactory RequestFactory { get; protected set; }

        public bool IsReady { get; protected set; }

        public bool IsOnline { get; protected set; }

        #endregion

        protected HttpWebRequestHandler RequestHandler;
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
            if (IsOnline) return true;
            if (LastCheck - DateTime.Now > RechckInterval) return false;
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
        
        public bool TryRequest<T>(IRequest request, out T response, string core = null) where T : class, IResponse
        {
            response = null;
            if (!IsOnline && !CheckStatus())
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
            if (!IsOnline && !CheckStatus()) throw new MizoreException("Server offline");
            return RequestHandler.Request<T>(request);
        }

        public T Request<T>(string type, string core = null) where T : class, IResponse
        {
            //TODO: how is the Data passed to the Request?
            var request = RequestFactory.CreateRequest(type,this);
            return RequestHandler.Request<T>(request);
        }
    }
}