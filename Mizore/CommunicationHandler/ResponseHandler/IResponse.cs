using System.IO;
using Mizore.CommunicationHandler.RequestHandler;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public interface IResponse
    {
        void Parse(Request request, Stream content);

        /// <summary>
        /// Request related to this response
        /// </summary>
        Request Request { get; }
    }
}