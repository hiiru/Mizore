using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.CommunicationHandler.RequestHandler;
using MizoreTests.Mock;

namespace MizoreTests.Tests.CommunicationHandler.Request
{
    [TestClass]
    public class PingRequestTest : RequestBaseTest
    {
        protected override IRequest CreateInstance()
        {
            return new PingRequest(new MockSolrServerHandler(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorArgumentNull()
        {
            new PingRequest(null);
        }
    }
}