using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class UpdateResponse : AResponseBase, IResponse
    {
        public UpdateResponse(UpdateRequest request, INamedList nl)
            : base(request, nl)
        { }
    }
}