using System.IO;
using Mizore.CommunicationHandler.Data;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.easynet_Javabin;
using Mizore.util;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class SelectResponse : AResponseBase, IResponse
    {
        public override void Parse(IRequest request, Stream content)
        {
            Request = request;
            Content = Request.Server.Serializer.Unmarshal(content);
        }

        //TODO: change this from SolrDocumentList to SelectResponseData
        private SelectResponseData _documents;

        public SelectResponseData Documents
        {
            get
            {
                if (_documents == null && Content != null)
                {
                    _documents = new SelectResponseData(Content.GetOrDefault<INamedList>("response"));
                }
                return _documents;
            }
        }
    }
}