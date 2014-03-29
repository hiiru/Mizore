using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.Data;
using Mizore.ContentSerializer.Data.Solr;

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
    }
}