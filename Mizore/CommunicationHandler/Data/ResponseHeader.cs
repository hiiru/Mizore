using Mizore.util;

namespace Mizore.CommunicationHandler.Data
{
    public class ResponseHeader
    {
        public int Status;
        public int QTime;
        public INamedList Parameters;
    }
}