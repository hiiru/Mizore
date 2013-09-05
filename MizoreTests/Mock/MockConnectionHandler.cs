using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ConnectionHandler;
using Mizore.Exceptions;
using MizoreTests.Resources;

namespace MizoreTests.Mock
{
    class MockConnectionHandler : IConnectionHandler
    {
        public string ResponseFilename = null;
        public string SerializerFormat = null;
        public string ResourcePath = null;

        public T Request<T>(IRequest request) where T : IResponse, new()
        {
            if (request==null) throw new ArgumentNullException("request");
            if (ResponseFilename == null) throw new MizoreException("ResponseFilename not set.");
            if (ResourcePath == null) throw new MizoreException("ResourcePath not set.");
            if (string.IsNullOrWhiteSpace(SerializerFormat))
            {
                if (request.Server == null) throw new ArgumentException("request.Server is null");
                if (request.Server.Serializer == null) throw new ArgumentException("request.Server.Serializer is null");
                if (string.IsNullOrWhiteSpace(request.Server.Serializer.wt)) throw new ArgumentException("request.Server.Serializer.wt is null or whitespace");
                SerializerFormat = request.Server.Serializer.wt;
            }
            string fileName = ResourcePath + ResponseFilename + "." + SerializerFormat;
            //if (!File.Exists(fileName)) throw new FileNotFoundException("ResponseFile for format not found.",fileName);
            //using (var fileStream = new BufferedStream(File.OpenRead(fileName)))
            using (var fileStream = ResourceProvider.GetResourceStream(fileName))
            {
                var response = new T();
                response.Parse(request, fileStream);
                return response;
            }
        }
    }
}
