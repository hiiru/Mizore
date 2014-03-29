using System.Collections.Generic;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public enum RequestMethod
    {
        GET,
        POST
    }

    public interface IRequest
    {
        /// <summary>
        /// Http Method
        /// This requires a value.
        /// </summary>
        RequestMethod Method { get; }

        /// <summary>
        /// Whole url + handler
        /// The Request handler is responsible for provinding the real url with all the query parameters.
        /// This requires a value.
        /// </summary>
        SolrUriBuilder UrlBuilder { get; }

        /// <summary>
        /// Prepared Stream for transmission.
        /// This will be ignored if it's null. (no content)
        /// </summary>
        INamedList Content { get; }

        /// <summary>
        /// Optional: Additional required HTTP headers can be provided here.
        /// These values should overwrite the ones provided by the ConnectionHandler.
        /// This will be ignored if it's null. (no additional headers)
        /// </summary>
        Dictionary<string, string> Header { get; }

        IResponse GetResponse(INamedList nl);

        /// <summary>
        /// Returns CacheKey for this request
        /// This should be unique.
        /// This will be ignored if it's null. (cache disabled)
        /// </summary>
        //string CacheKey { get; }
    }
}