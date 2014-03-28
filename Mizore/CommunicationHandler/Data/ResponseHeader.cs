using Mizore.Data;

namespace Mizore.CommunicationHandler.Data
{
    public class ResponseHeader
    {
        public ResponseHeader(INamedList responseHeader)
        {
            if (responseHeader.IsNullOrEmpty()) return;
            Status = responseHeader.GetOrDefaultStruct<int>("status");
            QTime = responseHeader.GetOrDefaultStruct<int>("QTime");
            Parameters = responseHeader.GetOrDefault<INamedList>("params");
        }

        public int Status;
        public int QTime;
        public INamedList Parameters;
    }
}