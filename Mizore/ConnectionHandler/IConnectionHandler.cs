using System;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer;

namespace Mizore.ConnectionHandler
{
    /// <summary>
    /// Low-level connection handler.
    /// Workload: This transfers the bit and bytes and handles header parsing.
    /// Requires: Request Instance -> Content which is transfered and destination information (url)
    /// e.g HTTP -> HttpWebRequest, WebClient, HttpClient...
    /// </summary>
    public interface IConnectionHandler
    {
        T Request<T>(IRequest request, IContentSerializerFactory serializerFactory) where T : IResponse;

        bool IsUriSupported(Uri uri);
    }
}