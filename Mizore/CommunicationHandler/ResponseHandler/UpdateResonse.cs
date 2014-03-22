using System.IO;
using Mizore.CommunicationHandler.Data;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.easynet_Javabin;
using Mizore.util;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class UpdateResponse : IResponse
    {
        public void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);
        }

        public IRequest Request { get; protected set; }

        public INamedList Content { get; protected set; }

        protected ResponseHeader _responseHeader;

        public ResponseHeader ResponseHeader
        {
            get
            {
                if (_responseHeader==null && Content != null)
                {
                    var head = Content.Get("responseHeader") as INamedList;
                    if (head != null)
                    {
                        var status = (int) head.Get("status");
                        var qtime = (int) head.Get("QTime");
                        var paramlist = head.Get("params") as INamedList;
                        _responseHeader = new ResponseHeader {Status = status, QTime = qtime, Parameters = paramlist};
                    }
                }
                return _responseHeader;
            }
        }

        public string Status { get; protected set; }
    }
}