using System.Web.UI;
using Mizore.CommunicationHandler.Data.Admin;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.CommunicationHandler.ResponseHandler.Admin;
using Mizore.ContentSerializer.Data;
using System.Collections.Generic;

namespace Mizore.CommunicationHandler.RequestHandler.Admin
{
    public class LoggingRequest : ARequestBaseGet
    {
        public LoggingRequest(SolrUriBuilder baseBuilder)
            : base(baseBuilder)
        {
            UrlBuilder.Core = "admin";
            UrlBuilder.Handler = "logging";
        }

        public LoggingRequest(SolrUriBuilder baseBuilder, Dictionary<string, string> logLevels)
            : this(baseBuilder)
        {
            // Note: Can't use typed loglevels because different solr versions or loggers may have different loglevels.
            // e.g. Solr 4.4: 
            // ALL, TRACE, DEBUG, INFO, WARN, ERROR, FATAL, OFF
            // However Documentation at https://cwiki.apache.org/confluence/display/solr/Configuring+Logging specifies:
            // FINEST, FINE, CONFIG, INFO, WARNING, SEVERE, OFF, UNSET
            foreach (var level in logLevels)
            {
                baseBuilder.Query.Add("set", string.Format("{0}:{1}", level.Key, level.Value));
            }
        }
        public LoggingRequest(SolrUriBuilder baseBuilder, long since)
            : this(baseBuilder)
        {
            baseBuilder.Query["since"] = since.ToString();
        }


        public override IResponse GetResponse(INamedList nl)
        {
            return new LoggingResponse(this, nl);
        }
    }
}