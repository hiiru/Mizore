﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.ContentSerializer;
using Mizore.SolrServerHandler;

namespace MizoreTests.Tests.SolrServerHandler
{
    public abstract class SolrServerHandlerBaseTest
    {
        protected abstract IContentSerializer CreateSerializer();

        protected abstract ISolrServerHandler CreateInstance();

        [TestMethod]
        public void MultiCorePropertyTest()
        {
            var server = CreateInstance();
            Assert.IsNotNull(server);
            Assert.IsNotNull(server.Cores);
            Assert.IsTrue(server.Cores.Count > 0);
            Assert.IsNotNull(server.DefaultCore);
        }

        [TestMethod]
        public void ServerAddressNotNull()
        {
            var server = CreateInstance();
            Assert.IsNotNull(server);
            Assert.IsNotNull(server.SolrUriBuilder);
            Assert.IsNotNull(server.SolrUriBuilder.ServerAddress);
        }

        [TestMethod]
        public void SerializerNotNull()
        {
            Assert.IsNotNull(CreateInstance().Serializer);
        }

        [TestMethod]
        public void RequestFactoryNotNull()
        {
            Assert.IsNotNull(CreateInstance().RequestFactory);
        }
    }
}