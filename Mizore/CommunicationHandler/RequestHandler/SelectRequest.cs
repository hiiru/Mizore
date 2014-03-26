using System;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class SelectRequest : ARequestBaseGet
    {
        public SelectRequest(ISolrServerHandler server, IQueryBuilder queryBuilder, string core = null)
            : base(server, core, "select")
        {
            if (queryBuilder == null)
                throw new ArgumentNullException("queryBuilder");
            uriBuilder.SetQuery(queryBuilder);
        }
    }
}