using System;
using System.Collections.Generic;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler.RequestHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ConnectionHandler;
using Mizore.ContentSerializer;

namespace Mizore.SolrServerHandler
{
    public class HttpSolrServer : ISolrServerHandler
    {
        public HttpSolrServer(string url, IContentSerializer contentSerializer = null, ICacheHandler cacheHandler=null)
        {
			  if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(url);
			  if (contentSerializer != null)
				  Serializer = contentSerializer;
			  else
				  //Init a default Serializer
				  Serializer = null;
			  if (cacheHandler != null)
				  Cache = cacheHandler;
			  else
				  //Init a default Cachehandler or keep disabled?
				  Cache = null;

			  //TODO-HIGH: url validation -> is it a valid solr url? what to do if not?
			  // Also Initialize related properties, (MulticoreMode,AdminCore,Cores,DefaultCore)
			  ServerAddress = url;

			  //TODO: Timeout?
        }

        public List<string> Cores
        {
            get { throw new NotImplementedException(); }
        }

        public string DefaultCore
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool MulticoreMode
        {
            get { return false; }
        }

        public bool AdminCore
        {
            get { return true; }
        }

	    public string ServerAddress { get; protected set; }

		 public ICacheHandler Cache { get; protected set; }

        public IContentSerializer Serializer{ get; protected set; }


        public int ConnectionTimeout
        {
            get { return 0; }
            set { throw new NotImplementedException(); }
        }

        public void Request(object obj, Request request, string core = null)
        {
            throw new NotImplementedException();
        }

        public bool Add()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public bool Commit(string core = null)
        {
            throw new NotImplementedException();
        }

        public bool Optimize(string core = null)
        {
            throw new NotImplementedException();
        }

        public PingResponse Ping()
        {
            HttpWebRequestHandler handler = new HttpWebRequestHandler();
            return handler.Request<PingResponse>(new PingRequest(this));
        }

        public void GetVersion()
        {
            throw new NotImplementedException();
        }
    }
}