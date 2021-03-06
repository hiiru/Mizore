﻿using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer;
using System.Collections.Generic;

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
        bool IsOnline { get; }

        /// <summary>
        /// Lists all the available cores.
        /// </summary>
        List<string> Cores { get; }

        /// <summary>
        /// Default core of the Connection, this is used in Multicore Mode.
        /// </summary>
        string DefaultCore { get; set; }

        ICacheHandler Cache { get; }

        IContentSerializerFactory SerializerFactory { get; }

        SolrUriBuilder GetUriBuilder(string core = null, string handler = null);

        bool TryRequest<T>(IRequest request, out T response, string core = null) where T : class, IResponse;

        T Request<T>(IRequest request, string core = null) where T : class, IResponse;
    }
}