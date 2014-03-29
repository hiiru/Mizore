using System;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.RequestHandler.Admin;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler
{
    public class RequestFactory : IRequestFactory
    {
        public IRequest CreateRequest(string requestType, SolrUriBuilder builder, INamedList content = null, IQueryBuilder queryBuilder = null)
        {
            if (string.IsNullOrWhiteSpace(requestType)) throw new ArgumentNullException("requestType");
            if (builder == null) throw new ArgumentNullException("builder");
            switch (requestType.ToLower())
            {
                //case "update":
                //    //TODO: how is the Data passed to the Request?
                //    return new UpdateRequest(server,core);
                case "ping":
                    return new PingRequest(builder);

                case "system":
                    return new SystemRequest(builder);

                case "cores":
                    return new CoresRequest(builder);

                case "select":
                    return new SelectRequest(builder, queryBuilder);

                default:
                    throw new NotImplementedException("RequestType " + requestType + " is not Implemented yet");
                    return null;
            }
        }
    }
}