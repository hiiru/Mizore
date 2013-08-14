using System.IO;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.ContentSerializer.easynet_Javabin;

namespace Mizore.CommunicationHandler.ResponseHandler
{
    public class PingResponse : IResponse
    {
        public void Parse(Request request, Stream content)
        {
            Request = request;

            //TODO-HIGH: Implement NamedList<T> and a ContentSerializer.
			  if (request.Server.Serializer!=null)
			  {
				  ContentENL=request.Server.Serializer.GetObject(content);
				  content.Position = 0;
			  }

            //Placeholder for testing
            using (var reader = new StreamReader(content))
					ContentString = reader.ReadToEnd();
        }

        public Request Request { get; protected set; }

        //Placeholder for testing
		  public string ContentString { get; set; }
		  public EasynetNamedList ContentENL{ get; set; }
    }
}