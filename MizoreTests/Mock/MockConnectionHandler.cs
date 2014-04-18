using System.Linq;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.Data.Params;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ConnectionHandler;
using Mizore.ContentSerializer;
using MizoreTests.Resources;
using System;
using System.IO;

namespace MizoreTests.Mock
{
    internal class MockConnectionHandler : IConnectionHandler
    {
        public T Request<T>(IRequest request, IContentSerializerFactory serializerFactory) where T : IResponse
        {
            if (request == null) throw new ArgumentNullException("request");
            if (request.UrlBuilder == null) throw new ArgumentException("Request doesn't have a UrlBuilder", "request");
            if (!request.UrlBuilder.Query.AllKeys.Contains(CommonParams.WT)) throw new ArgumentException("UrlBuilder doesn't have a WT set, it's required for testing!", "request");
            if (serializerFactory == null) throw new ArgumentNullException("serializerFactory");

            var fileSuffix = GetSuffix(request);

            Stream ms = null;
            using (var responseStream = GetFileStream(request.UrlBuilder, fileSuffix))
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
                throw new InvalidOperationException("No Matching ContentSerializer found for type " + contentType);
            var nl = serializer.Deserialize(ms);
            return (T)request.GetResponse(nl);
        }

        private string GetSuffix(IRequest request)
        {
            switch (string.Format("{0}_{1}", request.UrlBuilder.Core, request.UrlBuilder.Handler))
            {
                case "admin_logging":
                    if (request.UrlBuilder.Query.AllKeys.Contains("set")) return "set";
                    return null;
            }
            return null;
        }

        private const string BaseResoucePath = "MizoreTests.Resources.MockServer";

        private Stream GetFileStream(SolrUriBuilder urlBuilder, string suffix)
        {
            var hander = suffix != null ? string.Format("{0}_{1}", urlBuilder.Handler, suffix) : urlBuilder.Handler;
            var resouece = string.Format("{0}.{1}.{2}.{3}", BaseResoucePath, urlBuilder.Core, hander, urlBuilder.Query[CommonParams.WT]);
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