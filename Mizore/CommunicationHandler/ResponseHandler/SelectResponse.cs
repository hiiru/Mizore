using System.Collections.Generic;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.Data;
using Mizore.ContentSerializer.Data.Solr;
using Mizore.DataMappingHandler;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class SelectResponse : AResponseBase, IResponse
    {
        public SelectResponse(SelectRequest request, INamedList nl)
            : base(request, nl)
        { }

        private SolrDocumentList _documents;

        public SolrDocumentList Documents
        {
            get
            {
                if (_documents == null && Content != null)
                {
                    _documents = Content.GetOrDefault<SolrDocumentList>("response");
                }
                return _documents;
            }
        }

        public IList<T> GetObjects<T>(IDataMappingHandler mapping) where T : class, new()
        {
            if (mapping == null) return null;
            var handler = mapping.GetMappingHandler<T>();
            if (handler == null) return null;
            var list = new List<T>(Documents.Count);
            foreach (var doc in Documents)
            {
                list.Add(handler.GetObject(doc));
            }
            return list;
        }
    }
}