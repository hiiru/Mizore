using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public interface IResponse
    {
        /// <summary>
        /// Request related to this response
        /// </summary>
        IRequest Request { get; }

        INamedList Content { get; }
    }
}