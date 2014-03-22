using System.Collections.Generic;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer;

namespace Mizore.SolrServerHandler
{
    /// <summary>
    /// The SolrServerHandler manages all information about a solrserver connection endpoint (e.g. single core).
    /// The handler manages:
    /// - which ConnectionHandler, CacheHandler and Serializer is used.
    /// - simplifies request handling (request/response management with simple Add, Delete, Query, Commit, etc commands)
    /// - optional additional logic (e.g. loadbalancing, client-side request queue, zookeeper negotiation)
    /// </summary>
    public interface ISolrServerHandler
    {
        bool IsReady { get; }

        /// <summary>
        /// Lists all the available cores.
        /// </summary>
        List<string> Cores { get; }

        /// <summary>
        /// Default core of the Connection, this is used in Multicore Mode.
        /// </summary>
        string DefaultCore { get; set; }
        
        string ServerAddress { get; }

        ICacheHandler Cache { get; }

        IContentSerializer Serializer { get; }

        IRequestFactory RequestFactory { get; }

        bool TryRequest<T>(IRequest request, out T response, string core = null) where T : class, IResponse;
        T Request<T>(IRequest request, string core = null) where T : class, IResponse;
        T Request<T>(string type, string core = null) where T : class, IResponse;
    }
}