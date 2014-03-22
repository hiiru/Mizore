using System.IO;
using Mizore.CommunicationHandler.RequestHandler;

namespace Mizore.CommunicationHandler.ResponseHandler.Admin
{
    public class PingResponse : AResponseBase, IResponse
    {
        public override void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);
        }

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