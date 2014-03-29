using System.IO;
using Mizore.CommunicationHandler.Data;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.Data;
using Mizore.Data.Solr;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class SelectResponse : AResponseBase, IResponse
    {
        public override void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);
        }

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