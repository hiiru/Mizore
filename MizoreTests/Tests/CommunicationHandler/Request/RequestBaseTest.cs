using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.CommunicationHandler.RequestHandler;

namespace MizoreTests.Tests.CommunicationHandler.Request
{
    public abstract class RequestBaseTest
    {
        protected abstract IRequest CreateInstance();

        [TestMethod]
        public void MethodNotNull()
        {
            Assert.IsNotNull(CreateInstance().Method);
        }

        [TestMethod]
        public void UrlNotNull()
        {
            Assert.IsNotNull(CreateInstance().UrlBuilder.Uri);
        }

        [TestMethod]
        public void ServerNotNull()
        {
            throw new Exception();
            //Assert.IsNotNull(CreateInstance().Server);
        }
    }
}