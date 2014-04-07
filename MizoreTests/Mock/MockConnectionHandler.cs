using System;
using System.IO;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.Data.Params;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ConnectionHandler;
using Mizore.ContentSerializer;
using Mizore.Exceptions;
using MizoreTests.Resources;

namespace MizoreTests.Mock
{
    internal class MockConnectionHandler : IConnectionHandler
    {
        public T Request<T>(IRequest request, IContentSerializerFactory serializerFactory) where T : IResponse
        {
            if (request == null) throw new ArgumentNullException("request");
            if (request.UrlBuilder==null) throw new ArgumentException("Request doens't have a UrlBuilder","request");
            if (!request.UrlBuilder.Query.ContainsKey(CommonParams.WT)) throw new ArgumentException("UrlBuilder doesn't have a WT set, it's required for testing!", "request");
            if (serializerFactory==null) throw new ArgumentNullException("serializerFactory");
            
            Stream ms = null;
            using (var responseStream = GetFileStream(request.UrlBuilder))
            {
                if (responseStream != null)
                {
                    ms = new MemoryStream();
                    responseStream.CopyTo(ms);
                    ms.Position = 0;
                }
            }
            var contentType = GetContentType(request.UrlBuilder);
            var serializer = serializerFactory.GetContentSerializer(contentType);
            if (serializer == null)
                throw new InvalidOperationException("No Matching ContentSerializer found for type " +contentType);
            var nl = serializer.Deserialize(ms);
            return (T)request.GetResponse(nl);
        }

        private const string BaseResoucePath = "MizoreTests.Resources.MockServer";
        private Stream GetFileStream(SolrUriBuilder urlBuilder)
        {
            var resouece = string.Format("{0}.{1}.{2}.{3}", BaseResoucePath, urlBuilder.Core, urlBuilder.Handler, urlBuilder.Query[CommonParams.WT]);
            return ResourceProvider.GetResourceStream(resouece);
        }

        private string GetContentType(SolrUriBuilder urlBuilder)
        {
            switch (urlBuilder.Query[CommonParams.WT].ToLowerInvariant())
            {
                case "invalid":
                    return "invalid";
                case "json":
                    return "text/plain";
                case "javabin":
                    return "application/javabin";
                case "xml":
                    return "text/xml";
                default: 
                    throw new NotSupportedException("Unsupported WT Type!");
            }
        }
        
        public bool IsUriSupported(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}