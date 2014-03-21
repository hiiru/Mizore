using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ConnectionHandler;

namespace MizoreTests.Tests.ConnectionHandler
{
    // TODO: can anything more be tested without requiring a server? if not, do we want tests which require a server to pass?
    public abstract class ConnectionHandlerBaseTest
    {
        protected abstract IConnectionHandler CreateInstance(); 

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentNull()
        {
            CreateInstance().Request<PingResponse>(null);
        }
    }
}
