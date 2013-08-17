using System.IO;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.util;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public interface IResponse
    {
        void Parse(IRequest request, Stream content);

        /// <summary>
        /// Request related to this response
        /// </summary>
        IRequest Request { get; }

        INamedList Content { get; }
    }
}