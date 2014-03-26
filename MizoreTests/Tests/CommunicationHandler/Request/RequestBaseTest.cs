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
            Assert.IsNotNull(CreateInstance().Url);
        }

        [TestMethod]
        public void ServerNotNull()
        {
            Assert.IsNotNull(CreateInstance().Server);
        }
    }
}