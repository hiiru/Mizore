using System;
using System.IO;
using System.Net;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;

namespace Mizore.ConnectionHandler
{
    internal class HttpWebRequestHandler : IConnectionHandler
    {
        protected string ETag = null;

        public T Request<T>(IRequest request) where T : IResponse, new()
        {
            if (request == null) throw new ArgumentNullException("request");
            if (request.Server.Cache != null)
                ETag = request.Server.Cache.GetETag(request.CacheKey);
            try
            {
                var webRequest = CreateWebRequest(request);

                //if new T() can't be used due to missing new()
                //var response = (T)Activator.CreateInstance(typeof(T));
                var response = new T();
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
                response.Parse(request, ms);
                return response;
            }
            catch (WebException we)
            {
                //TODO: JIRA SOLR-7: Http exceptionhandling
                //TODO: JIIRA SOLR-9: Cache handling
                //TODO-LOW: JIRA SOLR-16: Solr errorpage parsing and own exception handling
                throw we;
            }
        }

        protected virtual HttpWebRequest CreateWebRequest(IRequest request)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(request.Url);
            webRequest.Method = request.Method;

            //Default settings
            webRequest.KeepAlive = true;
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            if (ETag != null) webRequest.Headers.Add(HttpRequestHeader.IfNoneMatch, ETag);

            if (request.Server.ConnectionTimeout > 0)
                webRequest.Timeout = webRequest.ReadWriteTimeout = request.Server.ConnectionTimeout;

            //TODO-LOW: UserAgent?
            //webRequest.UserAgent = "TODO";

            //TODO-LOW: ServicePoint.Expect100Continue for post values -> true or false? which is faster in the solr case?

            if (request.Content != null)
            {
                using (var requestStream = webRequest.GetRequestStream())
                {
                    request.Server.Serializer.Marshal(request.Content, requestStream);
                }
            }
            return webRequest;
        }
    }
}