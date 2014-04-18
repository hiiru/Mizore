using System.Collections;
using Mizore.CommunicationHandler.Data.Admin;
using Mizore.CommunicationHandler.RequestHandler.Admin;
using Mizore.ContentSerializer.Data;
using Mizore.ContentSerializer.Data.Solr;
using Mizore.DataMappingHandler;
using System.Collections.Generic;

namespace Mizore.CommunicationHandler.ResponseHandler.Admin
{
    public class LoggingResponse : AResponseBase, IResponse
    {
        public LoggingResponse(LoggingRequest request, INamedList nl)
            : base(request, nl)
        { }

        protected string _watcher;

        public string Watcher
        {
            get { return _watcher ?? (_watcher = Content.GetOrDefault<string>("watcher")); }
        }

        public List<string> Levels { get { return Info.Levels; } }

        protected LoggingInfo _info;

        public LoggingInfo Info
        {
            get
            {
                if (_info == null)
                {
                    var infoNl=Content.GetOrDefault<INamedList>("info");
                    if (infoNl == null)
                        _info = new LoggingInfo(Content.GetOrDefault<IList>("levels"));
                    else
                        _info = new LoggingInfo(infoNl);
                    
                }
                return _info;
            }
        }

        protected LoggingLoggers _loggers;

        public LoggingLoggers Loggers
        {
            get { return _loggers ?? (_loggers = new LoggingLoggers(Content.GetOrDefault<IList>("loggers"))); }
        }

        private SolrDocumentList _history;

        public SolrDocumentList History
        {
            get
            {
                if (_history == null && Content != null)
                {
                    _history = Content.GetOrDefault<SolrDocumentList>("history");
                }
                return _history;
            }
        }

        public IList<T> GetHistoryObjects<T>(IDataMappingHandler mapping) where T : class, new()
        {
            if (mapping == null) return null;
            var handler = mapping.GetMappingHandler<T>();
            if (handler == null) return null;
            var list = new List<T>(History.Count);
            foreach (var doc in History)
            {
                list.Add(handler.GetObject(doc));
            }
            return list;
        }
    }
}