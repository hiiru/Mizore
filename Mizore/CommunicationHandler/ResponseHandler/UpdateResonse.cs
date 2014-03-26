using System.IO;
using Mizore.CommunicationHandler.RequestHandler;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class UpdateResponse : AResponseBase, IResponse
    {
        public override void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);
        }
    }
}