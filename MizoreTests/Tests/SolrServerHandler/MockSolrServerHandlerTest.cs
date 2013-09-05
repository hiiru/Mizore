using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.ContentSerializer;
using Mizore.SolrServerHandler;
using MizoreTests.Mock;

namespace MizoreTests.Tests.SolrServerHandler
{
    public abstract class MockSolrServerHandlerTest : SolrServerHandlerBaseTest
    {
        protected override ISolrServerHandler CreateInstance()
        {
            return new MockSolrServerHandler(resourcePath: "MizoreTests.Resources.ResponseFiles.", contentSerializer: CreateSerializer());
        }

        [TestMethod]
        public void Ping()
        {
            var server = CreateInstance();
            var response = server.Ping();
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Status, "OK");
            Assert.AreEqual(response.ResponseHeader.QTime, 1);
            Assert.AreEqual(response.ResponseHeader.Status, 0);
        }
    }

    [TestClass]
    public class MockSolrServerHandlerTest_EasynetJavabin : MockSolrServerHandlerTest
    {
        protected override IContentSerializer CreateSerializer()
        {
            return new EasynetJavabinSerializer();
        }
    }
}
