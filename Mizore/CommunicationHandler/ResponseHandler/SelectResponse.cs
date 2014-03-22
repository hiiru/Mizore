using System.IO;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class SelectResponse : AResponseBase, IResponse
    {
        public override void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);
        }

    }
}