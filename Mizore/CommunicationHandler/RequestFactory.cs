using System;
using System.Linq;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.RequestHandler.Admin;
using Mizore.SolrServerHandler;

namespace Mizore.CommunicationHandler
{
    public class RequestFactory : IRequestFactory
    {
        public IRequest CreateRequest(string requestType, ISolrServerHandler server, string core, params object[] objects)
        {
            if (core == null) core = server.DefaultCore;
            switch (requestType.ToLower())
            {
                //case "update":
                //    //TODO: how is the Data passed to the Request?
                //    return new UpdateRequest(server,core);
                case "ping":
                    return new PingRequest(server);
                case "system":
                    return new SystemRequest(server);
                case "cores":
                    return new CoresRequest(server);
                case "select":
                    return new SelectRequest(server,objects[0] as string, core);
                default:
                    throw new NotImplementedException("RequestType " + requestType + " is not Implemented yet");
                    return null;
            }
        }

        public IRequest CreateRequest(string requestType, ISolrServerHandler server, params object[] objects)
        {
            return CreateRequest(requestType, server, null, objects);
        }
    }
}