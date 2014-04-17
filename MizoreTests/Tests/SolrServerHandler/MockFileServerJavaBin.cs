﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.SolrServerHandler;
using MizoreTests.Mock;

namespace MizoreTests.Tests.SolrServerHandler
{
    [TestClass]
    public class MockFileServerJavaBin : TestSolrServerHandlerBase
    {
        public MockFileServerJavaBin()
        {
            _server = new MockSolrServerHandler();
        }

        private readonly MockSolrServerHandler _server;

        protected override ISolrServerHandler Server { get { return _server; } }

        protected override string WTValue
        {
            get { return "json"; }
        }
    }
}