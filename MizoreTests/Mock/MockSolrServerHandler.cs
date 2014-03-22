using System;
using System.Collections.Generic;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ContentSerializer;
using Mizore.SolrServerHandler;

namespace MizoreTests.Mock
{
    public class MockSolrServerHandler : ISolrServerHandler
    {
        protected string ResourcePath;

        public MockSolrServerHandler(string resourcePath, string url=null, IContentSerializer contentSerializer = null, ICacheHandler cacheHandler = null, IRequestFactory factory = null)
        {
            ResourcePath = resourcePath;
            ServerAddress = url ?? "http://127.0.0.1:20440/solr/";
            Serializer = contentSerializer ?? new EasynetJavabinSerializer();
            Cache = cacheHandler ?? null;
            RequestFactory = factory ?? new RequestFactory();
            DefaultCore = "mizoreMockingTestCore";
            Cores = new List<string> { DefaultCore };
        }

        public bool IsReady { get; private set; }
        public List<string> Cores { get; private set; }

        public string DefaultCore { get; set; }

        public bool MulticoreMode { get; private set; }

        public string ServerAddress { get; private set; }

        public ICacheHandler Cache { get; private set; }

        public IContentSerializer Serializer { get; private set; }

        public IRequestFactory RequestFactory { get; private set; }
        public bool TryRequest<T>(IRequest request, out T response, string core = null) where T : class, IResponse
        {
            throw new NotImplementedException();
        }

        public T Request<T>(IRequest request, string core = null) where T : class, IResponse
        {
            throw new NotImplementedException();
        }

        public T Request<T>(string type, string core = null) where T : class, IResponse
        {
            throw new NotImplementedException();
        }

        public int ConnectionTimeout { get; set; }

        public UpdateResponse Add(string core = null)
        {
            throw new NotImplementedException();
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
            var conHandler = new MockConnectionHandler { ResponseFilename = "ping", ResourcePath = ResourcePath};
            return conHandler.Request<PingResponse>(RequestFactory.CreateRequest("ping", this));
        }

        public SystemResponse GetSystemInfo()
        {
            throw new NotImplementedException();
        }

        public void GetVersion()
        {
            throw new NotImplementedException();
        }
    }
}