using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer;
using Mizore.SolrServerHandler;
using System.Collections.Generic;

namespace MizoreTests.Mock
{
    public class MockSolrServerHandler : ISolrServerHandler
    {
        private readonly SolrUriBuilder baseUriBuilder;
        private readonly MockConnectionHandler RequestHandler;

        public MockSolrServerHandler(IContentSerializerFactory contentSerializerFactory = null, ICacheHandler cacheHandler = null)
        {
            baseUriBuilder = new SolrUriBuilder(null ?? "http://127.0.0.1:20440/solr/");
            RequestHandler = new MockConnectionHandler();
            SerializerFactory = contentSerializerFactory ?? new ContentSerializerFactory();
            Cache = cacheHandler;
            DefaultCore = "MockCore";
            Cores = new List<string> { DefaultCore };
            IsReady = true;
        }

        public bool IsReady { get; private set; }

        public bool IsOnline { get { return true; } }

        public List<string> Cores { get; private set; }

        public string DefaultCore { get; set; }

        public ICacheHandler Cache { get; private set; }

        public IContentSerializerFactory SerializerFactory { get; private set; }

        public SolrUriBuilder GetUriBuilder(string core = null, string handler = null)
        {
            return baseUriBuilder.GetBuilder(core ?? DefaultCore, handler);
        }

        public bool TryRequest<T>(IRequest request, out T response, string core = null) where T : class, IResponse
        {
            response = null;
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
            return RequestHandler.Request<T>(request, SerializerFactory);
        }
    }
}