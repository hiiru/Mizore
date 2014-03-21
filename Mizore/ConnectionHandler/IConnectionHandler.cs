using System;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;

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
        /// <summary>
        /// Transfers the Request to the Solr Server and returns it's Response.
        /// </summary>
        /// <typeparam name="T">IResponse type, which handles the response.</typeparam>
        /// <param name="request">IRequest implementation, which handles the required date for the Request.</param>
        /// <exception cref="MizoreConnectionExcpetion">Thrown when a problem with the Conneection to the server occurs</exception>
        /// <returns>IResponse implementation for the Response</returns>
        T Request<T>(IRequest request) where T : IResponse;

        bool IsUriSupported(Uri uri);
    }
}