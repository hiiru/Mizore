using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.Data.Admin
{
    public class SystemLuceneData
    {
        public SystemLuceneData(INamedList responseHeader)
        {
            if (responseHeader.IsNullOrEmpty()) return;
            SolrSpecVersion = responseHeader.GetOrDefault<string>("solr-spec-version");
            SolrImplVersion = responseHeader.GetOrDefault<string>("solr-impl-version");
            LuceneSpecVersion = responseHeader.GetOrDefault<string>("lucene-spec-version");
            LuceneImplVersion = responseHeader.GetOrDefault<string>("lucene-impl-version");
        }

        public string SolrSpecVersion { get; protected set; }

        public string SolrImplVersion { get; protected set; }

        public string LuceneSpecVersion { get; protected set; }

        public string LuceneImplVersion { get; protected set; }
    }
}