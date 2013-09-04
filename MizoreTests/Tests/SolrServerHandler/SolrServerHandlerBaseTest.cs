using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.SolrServerHandler;

namespace MizoreTests.Tests.SolrServerHandler
{
    public abstract class SolrServerHandlerBaseTest
    {
        protected abstract ISolrServerHandler CreateInstance();

        [TestMethod]
        public void MultiCorePropertyTest()
        {
            var server = CreateInstance();
            Assert.IsNotNull(server);
            if (server.MulticoreMode)
            {
                Assert.IsNotNull(server.Cores);
                Assert.IsTrue(server.Cores.Count > 0);
                Assert.IsNotNull(server.DefaultCore);
            }
        }
        [TestMethod]
        public void ServerAddressNotNull()
        {
            var server = CreateInstance();
            Assert.IsNotNull(server);
            Assert.IsFalse(string.IsNullOrWhiteSpace(server.ServerAddress));
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