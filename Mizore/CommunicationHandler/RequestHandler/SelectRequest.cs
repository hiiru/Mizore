﻿using System;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.RequestHandler
{
    public class SelectRequest : ARequestBaseGet
    {
        public SelectRequest(SolrUriBuilder builder, IQueryBuilder queryBuilder)
            //ISolrServerHandler server, IQueryBuilder queryBuilder, string core = null)
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