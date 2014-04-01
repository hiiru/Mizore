using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.Data;
using Mizore.ContentSerializer.Data.Solr;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class GetResponse : IResponse
    {
        public GetResponse(GetRequest request, INamedList nl)
        {
            Request = request;
            Content = nl;
        }

        public IRequest Request { get; protected set; }

        public INamedList Content { get; protected set; }

        private SolrDocument _document;
        private bool docIsNull;
        public SolrDocument Document
        {
            get
            {
                if (!docIsNull && _document == null && Content != null)
                {
                    var docNl = Content.GetOrDefault<INamedList>("doc");
                    if (docNl!=null)
                        _document = new SolrDocument(docNl);
                    else
                        docIsNull = true;
                }
                return _document;
            }
        }
    }
}
