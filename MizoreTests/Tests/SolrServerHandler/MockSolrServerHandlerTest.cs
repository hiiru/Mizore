using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.SolrServerHandler;
using MizoreTests.Mock;

namespace MizoreTests.Tests.SolrServerHandler
{
    [TestClass]
    public class MockSolrServerHandlerTest : SolrServerHandlerBaseTest
    {
        protected override ISolrServerHandler CreateInstance()
        {
            return new MockSolrServerHandler();
        }
    }
}
