using Mizore.CommunicationHandler.RequestHandler.Admin;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.ResponseHandler.Admin
{
    public class PingResponse : AResponseBase, IResponse
    {
        public PingResponse(PingRequest request, INamedList nl)
            : base(request, nl)
        { }

        protected string _status;

        public string Status
        {
            get
            {
                if (_status == null)
                {
                    _status = Content != null ? Content.GetOrDefault<string>("status") : "FAIL";
                }
                return _status;
            }
        }
    }
}