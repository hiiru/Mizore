using System;
using System.Collections.Specialized;
using Mizore.Data;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public interface IRequest
    {
        /// <summary>
        /// Http Method
        /// This requires a value.
        /// </summary>
        string Method { get; }

        /// <summary>
        /// Whole url + handler
        /// The Request handler is responsible for provinding the real url with all the query parameters.
        /// This requires a value.
        /// </summary>
        Uri Url { get; }

        /// <summary>
        /// Returns the server instance for this request.
        /// This requires a value.
        /// </summary>
        ISolrServerHandler Server { get; }

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
        NameValueCollection Header { get; }

        /// <summary>
        /// Returns CacheKey for this request
        /// This should be unique.
        /// This will be ignored if it's null. (cache disabled)
        /// </summary>
        string CacheKey { get; }
    }
}