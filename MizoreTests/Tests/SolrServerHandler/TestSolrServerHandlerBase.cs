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
        public void RequestPingJSON()
        {
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = "json";
            var ping = Server.Request<PingResponse>(new PingRequest(builder));
            Assert.IsNotNull(ping);
            Assert.IsTrue(ping.Status == "OK");
            Assert.IsNotNull(ping.ResponseHeader);
            Assert.IsNotNull(ping.Request);
        }

        [TestMethod]
        [Priority(3)]
        public void TryRequestPingJSON()
        {
            PingResponse ping;
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = "json";
            Assert.IsTrue(Server.TryRequest<PingResponse>(new PingRequest(builder), out ping));
            Assert.IsNotNull(ping);
            Assert.IsTrue(ping.Status == "OK");
            Assert.IsNotNull(ping.ResponseHeader);
            Assert.IsNotNull(ping.Request);
        }

        [TestMethod]
        [Priority(3)]
        public void RequestPingJavaBin()
        {
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = "javabin";
            var ping = Server.Request<PingResponse>(new PingRequest(builder));
            Assert.IsNotNull(ping);
            Assert.IsTrue(ping.Status == "OK");
            Assert.IsNotNull(ping.ResponseHeader);
            Assert.IsNotNull(ping.Request);
        }

        [TestMethod]
        [Priority(3)]
        public void TryRequestPingJavaBin()
        {
            PingResponse ping;
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = "javabin";
            Assert.IsTrue(Server.TryRequest<PingResponse>(new PingRequest(builder), out ping));
            Assert.IsNotNull(ping);
            Assert.IsTrue(ping.Status == "OK");
            Assert.IsNotNull(ping.ResponseHeader);
            Assert.IsNotNull(ping.Request);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No Matching ContentSerializer found for type text/xml")]
        [Priority(3)]
        public void RequestPingInvalid()
        {
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = "invalid";
            var ping = Server.Request<PingResponse>(new PingRequest(builder));
            Assert.IsNotNull(ping);
            Assert.IsTrue(ping.Status == "OK");
            Assert.IsNotNull(ping.ResponseHeader);
            Assert.IsNotNull(ping.Request);
        }

        [TestMethod]
        [Priority(3)]
        public void TryRequestPingInvalid()
        {
            PingResponse ping;
            var builder = Server.GetUriBuilder();
            builder.Query[CommonParams.WT] = "invalid";
            Assert.IsFalse(Server.TryRequest<PingResponse>(new PingRequest(builder), out ping));
            Assert.IsNull(ping);
        }
    }
}