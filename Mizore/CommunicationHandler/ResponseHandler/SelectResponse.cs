using Mizore.CommunicationHandler.Data;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.Data;
using Mizore.ContentSerializer.Data.Solr;
using Mizore.DataMappingHandler;
using System.Collections.Generic;

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

        private FacetData _facets;

        public FacetData Facets
        {
            get
            {
                if (_facets == null && Content != null)
                {
                    _facets = new FacetData(Content.GetOrDefault<INamedList>("facet_counts"));
                }
                return _facets;
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