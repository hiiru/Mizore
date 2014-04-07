using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.SolrServerHandler;
using MizoreTests.Mock;

namespace MizoreTests.Tests.SolrServerHandler
{
    [TestClass]
    public class MockFileServer : TestSolrServerHandlerBase
    {
        public MockFileServer()
        {
            _server = new MockSolrServerHandler();
        }

        private readonly MockSolrServerHandler _server;

        protected override ISolrServerHandler Server { get { return _server; } }
    }
}