using System.Linq;
using Mizore.CommunicationHandler.Data.Params;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer;
using Mizore.Exceptions;
using System;
using System.IO;
using System.Net;

namespace Mizore.ConnectionHandler
{
    public class HttpWebRequestHandler : IConnectionHandler
    {
        /// <summary>
        /// Transfers the Request to the Solr Server and returns it's Response.
        /// </summary>
        /// <typeparam name="T">IResponse type, which handles the response.</typeparam>
        /// <param name="request">IRequest implementation, which handles the required date for the Request.</param>
        /// <exception cref="MizoreConnectionExcpetion">Thrown when a problem with the Connection to the server occurs</exception>
        /// <returns>IResponse implementation for the Response</returns>
        public T Request<T>(IRequest request, IContentSerializerFactory serializerFactory) where T : IResponse
        {
            if (request == null) throw new ArgumentNullException("request");
            if (serializerFactory == null) throw new ArgumentNullException("serializerFactory");

            //if (request.Server.Cache != null)
            //    ETag = request.Server.Cache.GetETag(request.CacheKey);
            try
            {
                var webRequest = CreateWebRequest(request, serializerFactory);
                var webResponse = webRequest.GetResponse();
                Stream ms = null;
                using (var responseStream = webResponse.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        ms = new MemoryStream();
                        responseStream.CopyTo(ms);
                        ms.Position = 0;
                    }
                }
                var serializer = serializerFactory.GetContentSerializer(webResponse.ContentType);
                if (serializer == null)
                    throw new InvalidOperationException("No Matching ContentSerializer found for type " + webResponse.ContentType);
                var nl = serializer.Deserialize(ms);
                return (T)request.GetResponse(nl);
            }
            catch (WebException we)
            {
                //TODO: JIRA MIZORE-7: Http exceptionhandling
                //TODO: JIRA MIZORE-9: Cache handling
                //TODO-LOW: JIRA MIZORE-16: Solr errorpage parsing and own exception handling
                throw new MizoreConnectionException(request, "Connection exception occured in HttpWebRequestHandler.", we);
            }
            catch (Exception e)
            {
                //TODO: generic error handling
                throw new MizoreConnectionException(request, e);
            }
        }

        public bool IsUriSupported(Uri uri)
        {
            if (uri == null || !uri.IsAbsoluteUri) return false;
            switch (uri.Scheme)
            {
                case "http":
                case "https":
                    break;

                default:
                    return false;
            }
            if (!string.IsNullOrWhiteSpace(uri.UserInfo)) return false;
            if (!string.IsNullOrWhiteSpace(uri.Query)) return false;
            return true;
        }

        protected virtual HttpWebRequest CreateWebRequest(IRequest request, IContentSerializerFactory serializerFactory)
        {
            var serializer = GetSerializer(request, serializerFactory);
            var wt = request.UrlBuilder.Query.Get(CommonParams.WT);
            if (wt==null)
                request.UrlBuilder.Query.Add(CommonParams.WT, serializer.WT);

            var webRequest = (HttpWebRequest)WebRequest.Create(request.UrlBuilder.Uri);
            webRequest.Method = request.Method.ToString("G");
            if (request.Method == RequestMethod.GET)
                webRequest.Accept = serializer.ContentType;
            else
                webRequest.ContentType = serializer.ContentType;

            //Default settings
            webRequest.KeepAlive = true;
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            webRequest.UserAgent = "Mizore (.NET solr library)";

            //TODO-LOW: ServicePoint.Expect100Continue for post values -> true or false? which is faster in the solr case?

            if (request.Content != null)
            {
                using (var requestStream = webRequest.GetRequestStream())
                {
                    serializer.Serialize(request.Content, requestStream);
                }
            }
            return webRequest;
        }

        protected IContentSerializer GetSerializer(IRequest request, IContentSerializerFactory serializerFactory)
        {
            string serializerType =request.UrlBuilder.Query.Get(CommonParams.WT);
            if (serializerType==null && request.Header != null && request.Header.ContainsKey("content-type"))
                serializerType = request.Header["content-type"];
            return serializerFactory.GetContentSerializer(serializerType);
        }
    }
}