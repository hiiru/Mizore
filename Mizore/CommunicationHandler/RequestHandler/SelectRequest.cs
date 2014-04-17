using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer.Data;
using System;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class SelectRequest : ARequestBaseGet
    {
        public SelectRequest(SolrUriBuilder builder, IQueryBuilder queryBuilder)
            : base(builder)
        {
            UrlBuilder.Handler = "select";
            if (queryBuilder == null)
                throw new ArgumentNullException("queryBuilder");
            UrlBuilder.SetQuery(queryBuilder);
        }

        public override IResponse GetResponse(INamedList nl)
        {
            return new SelectResponse(this, nl);
        }
    }
}