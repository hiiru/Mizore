using System.IO;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.easynet_Javabin;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class PingResponse : IResponse
    {
        public void Parse(IRequest request, Stream content)
        {
            Request = request;

            //TODO-HIGH: Implement NamedList<T> and a ContentSerializer.
            if (request.Server.Serializer != null)
            {
                ContentENL = request.Server.Serializer.Unmarshal(content) as EasynetNamedList;
                content.Position = 0;
            }

            //Placeholder for testing
            using (var reader = new StreamReader(content))
                ContentString = reader.ReadToEnd();
        }

        public IRequest Request { get; protected set; }

        //Placeholder for testing
        public string ContentString { get; set; }

        public EasynetNamedList ContentENL { get; set; }
    }
}