using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.RequestHandler.Admin;

namespace MizoreTests.Tests.CommunicationHandler.Request
{
    [TestClass]
    public class PingRequestTest : RequestBaseTest
    {
        protected override IRequest CreateInstance()
        {
            //TODO: fix unit tests for new communication->Content->Connection design
            return null;
            //return new PingRequest(new MockSolrServerHandler(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorArgumentNull()
        {
            new PingRequest(null);
        }
    }
}