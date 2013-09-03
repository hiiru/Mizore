using System;
using System.IO;
using System.Net;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.Exceptions;

namespace Mizore.ConnectionHandler
{
    public class HttpWebRequestHandler : IConnectionHandler
    {
        protected string ETag = null;

        /// <summary>
        /// Transfers the Request to the Solr Server and returns it's Response.
        /// </summary>
        /// <typeparam name="T">IResponse type, which handles the response.</typeparam>
        /// <param name="request">IRequest implementation, which handles the required date for the Request.</param>
        /// <exception cref="MizoreConnectionExcpetion">Thrown when a problem with the Conneection to the server occurs</exception>
        /// <returns>IResponse implementation for the Response</returns>
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
                throw new MizoreConnectionException(request, "Connection exception occured in HttpWebRequestHandler.", we);
            }
            catch (Exception e)
            {
                //TODO: generic error handling
                throw new MizoreConnectionException(request, e);
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