using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.CommunicationHandler.RequestHandler;

namespace MizoreTests.Tests.CommunicationHandler.Request
{
    [TestClass]
    public class PingRequestTest : RequestBaseTest
    {
        protected override IRequest CreateInstance()
        {
            //TODO: fix when a mocking SolrServerHandler is available
            return new PingRequest(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorArgumentNull()
        {
            new PingRequest(null);
        }
    }
}