using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.CommunicationHandler.Data.Params;
using Mizore.CommunicationHandler.RequestHandler.Admin;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.SolrServerHandler;

namespace MizoreTests.Tests.SolrServerHandler
{
    public abstract class TestSolrServerHandlerBase
    {
        protected abstract ISolrServerHandler Server { get; }

        protected abstract string WTValue { get; }

        [TestMethod]
        [Priority(0)]
        public void ServerIsNotNull()
        {
            Assert.IsNotNull(Server);
        }

        [TestMethod]
        [Priority(1)]
        public void ServerIsReady()
        {
            Assert.IsTrue(Server.IsReady);
        }

        [TestMethod]
        [Priority(1)]
        public void ServerHasSerializerFactory()
        {
            Assert.IsNotNull(Server.SerializerFactory);
        }

        [TestMethod]
        [Priority(1)]
        public void DefaultCoreIsValid()
        {
            Assert.IsNotNull(Server.DefaultCore);
            Assert.IsTrue(Server.Cores.Contains(Server.DefaultCore));
        }

        [TestMethod]
        [Priority(2)]
        public void ServerGetUrlBuilder()
        {
            var builder = Server.GetUriBuilder();
            Assert.IsNotNull(builder);
            Assert.IsFalse(builder.IsBaseUrl);
            Assert.IsTrue(builder.Core == Server.DefaultCore);

            var builder2 = Server.GetUriBuilder("testcore", "testhandler");
            Assert.IsNotNull(builder2);
            Assert.IsTrue(builder2.Core == "testcore");
            Assert.IsTrue(builder2.Handler == "testhandler");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Priority(2)]
        public void RequestNull()
        {
            Server.Request<IResponse>(null);
        }

        [TestMethod]
        [Priority(2)]
        public void TryRequestNull()
        {
            IResponse outResponse;
            Assert.IsFalse(Server.TryRequest<IResponse>(null, out outResponse));
            Assert.IsNull(outResponse);
        }

        [TestMethod]
        [Priority(3)]
        public void RequestPing()
        {
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = WTValue;
            var ping = Server.Request<PingResponse>(new PingRequest(builder));
            Assert.IsNotNull(ping);
            Assert.IsTrue(ping.Status == "OK");
            Assert.IsNotNull(ping.ResponseHeader);
            Assert.IsNotNull(ping.Request);
        }

        [TestMethod]
        [Priority(3)]
        public void TryRequestPing()
        {
            PingResponse ping;
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = WTValue;
            Assert.IsTrue(Server.TryRequest<PingResponse>(new PingRequest(builder), out ping));
            Assert.IsNotNull(ping);
            Assert.IsTrue(ping.Status == "OK");
            Assert.IsNotNull(ping.ResponseHeader);
            Assert.IsNotNull(ping.Request);
        }
        
        [TestMethod]
        [Priority(3)]
        public void RequestCores()
        {
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = WTValue;
            var cores = Server.Request<CoresResponse>(new CoresRequest(builder));
            Assert.IsNotNull(cores);
            Assert.IsNotNull(cores.Cores);
            Assert.IsFalse(cores.DefaultCore == null);
            Assert.IsNotNull(cores.ResponseHeader);
            Assert.IsNotNull(cores.Request);
        }
    }
}