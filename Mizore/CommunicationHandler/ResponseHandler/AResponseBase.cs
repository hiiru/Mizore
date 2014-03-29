using Mizore.CommunicationHandler.Data;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public abstract class AResponseBase : IResponse
    {
        public AResponseBase(IRequest request, INamedList list)
        {
            Request = request;
            Content = list;
        }

        public IRequest Request { get; protected set; }

        public INamedList Content { get; protected set; }

        protected ResponseHeader _responseHeader;

        public ResponseHeader ResponseHeader
        {
            get
            {
                if (_responseHeader == null && Content != null)
                {
                    _responseHeader = new ResponseHeader(Content.GetOrDefault<INamedList>("responseHeader"));
                }
                return _responseHeader;
            }
        }
    }
}