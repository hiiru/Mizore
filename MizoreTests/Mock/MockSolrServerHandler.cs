using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mizore.CacheHandler;
using Mizore.CommunicationHandler;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer;
using Mizore.SolrServerHandler;

namespace MizoreTests.Mock
{
    public class MockSolrServerHandler : ISolrServerHandler
    {
        public MockSolrServerHandler() {}

        public List<string> Cores { get; private set; }
        public string DefaultCore { get; set; }
        public bool MulticoreMode { get; private set; }
        public string ServerAddress { get; private set; }
        public ICacheHandler Cache { get; private set; }
        public IContentSerializer Serializer { get; private set; }
        public IRequestFactory RequestFactory { get; private set; }
        public int ConnectionTimeout { get; set; }
        public UpdateResponse Add(string core = null)
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
            throw new NotImplementedException();
        }

        public SystemResponse GetSystemInfo()
        {
            throw new NotImplementedException();
        }

        public void GetVersion()
        {
            throw new NotImplementedException();
        }
    }
}
