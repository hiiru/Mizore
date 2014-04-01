using System;
using System.Collections.Generic;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.RequestHandler.Admin;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ContentSerializer;
using Mizore.SolrServerHandler;

namespace MizoreTests.Mock
{
    public class MockSolrServerHandler : ISolrServerHandler
    {
        protected string ResourcePath;
        private readonly SolrUriBuilder baseUriBuilder;

        public MockSolrServerHandler(string resourcePath, IContentSerializerFactory contentSerializerFactory = null, ICacheHandler cacheHandler = null)
        {
            ResourcePath = resourcePath;
            baseUriBuilder = new SolrUriBuilder(null ?? "http://127.0.0.1:20440/solr/");
            SerializerFactory = contentSerializerFactory ?? new ContentSerializerFactory();
            Cache = cacheHandler ?? null;
            DefaultCore = "mizoreMockingTestCore";
            Cores = new List<string> { DefaultCore };
        }

        public bool IsReady { get; private set; }

        public List<string> Cores { get; private set; }

        public string DefaultCore { get; set; }

        public bool MulticoreMode { get; private set; }

        public ICacheHandler Cache { get; private set; }

        public IContentSerializerFactory SerializerFactory { get; private set; }

        public SolrUriBuilder GetUriBuilder(string core = null, string handler = null)
        {
            return baseUriBuilder.GetBuilder(core ?? DefaultCore, handler);
        }

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
            var conHandler = new MockConnectionHandler { ResponseFilename = "ping", ResourcePath = ResourcePath };
            return conHandler.Request<PingResponse>(new PingRequest(GetUriBuilder()), SerializerFactory);
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