using System;
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
        public string ResponseFilename = null;
        public string SerializerFormat = null;
        public string ResourcePath = null;

        public T Request<T>(IRequest request, IContentSerializerFactory serializerFactory) where T : IResponse
        {
            if (request == null) throw new ArgumentNullException("request");
            if (ResponseFilename == null) throw new MizoreException("ResponseFilename not set.");
            if (ResourcePath == null) throw new MizoreException("ResourcePath not set.");
            string serializerType = null;
            if (request.UrlBuilder.Query.ContainsKey(CommonParams.WT))
                serializerType = request.UrlBuilder.Query[CommonParams.WT];
            else if (request.Header.ContainsKey("content-type"))
                serializerType = request.Header["content-type"];
            var serializer = serializerFactory.GetContentSerializer(serializerType);
            if (serializer == null)
                throw new InvalidOperationException("No Matching ContentSerializer found.");
            if (string.IsNullOrWhiteSpace(SerializerFormat))
            {
                throw new Exception();
                //if (request.Server == null) throw new ArgumentException("request.Server is null");
                //if (request.Server.Serializer == null) throw new ArgumentException("request.Server.Serializer is null");
                //if (string.IsNullOrWhiteSpace(request.Server.Serializer.wt)) throw new ArgumentException("request.Server.Serializer.wt is null or whitespace");
                //SerializerFormat = request.Server.Serializer.wt;
            }
            string fileName = ResourcePath + ResponseFilename + "." + SerializerFormat;
            //if (!File.Exists(fileName)) throw new FileNotFoundException("ResponseFile for format not found.",fileName);
            //using (var fileStream = new BufferedStream(File.OpenRead(fileName)))
            using (var fileStream = ResourceProvider.GetResourceStream(fileName))
            {
                var nl = serializer.Deserialize(fileStream);
                return (T)request.GetResponse(nl);
            }
        }

        public bool IsUriSupported(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}