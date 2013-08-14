using System.Collections.Generic;
using Mizore.CacheHandler;
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
        /// <summary>
        /// Lists all the available cores.
        /// </summary>
        List<string> Cores { get; }

        /// <summary>
        /// Default core of the Connection, this is used in Multicore Mode.
        /// </summary>
        string DefaultCore { get; set; }

        /// <summary>
        /// This returns true if multicore support is available.
        /// </summary>
        ///
        bool MulticoreMode { get; }

        /// <summary>
        /// This should return true if /admin can be used on the server (e.g. ping)
        /// </summary>
        bool AdminCore { get; }

        string ServerAddress { get; }

        ICacheHandler Cache { get; }

        IContentSerializer Serializer { get; }

        int ConnectionTimeout { get; set; }

        // is this needed? or same as timeout?
        //int ConnectionReadWriteTimeout { get;set; }

        //Input: object/doc/namedlist / Optional: core
        bool Add();

        //Input: id/query / Optional: core
        bool Delete();

        //No Input / Optional: core
        bool Commit(string core = null);

        bool Optimize(string core = null);

        PingResponse Ping();

        void GetVersion();

        //future: admin features (cores, etc.) and statistics
    }
}