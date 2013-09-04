using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.ConnectionHandler;

namespace MizoreTests.Tests.ConnectionHandler
{
    [TestClass]
    public class HttpWebRequestHandlerTest : ConnectionHandlerBaseTest
    {
        protected override IConnectionHandler CreateInstance()
        {
            return new HttpWebRequestHandler();
        }
    }
}