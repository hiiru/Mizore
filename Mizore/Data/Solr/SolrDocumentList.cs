using System.Collections.Generic;

namespace Mizore.Data.Solr
{
    public class SolrDocumentList : List<SolrDocument>
    {
        public long NumFound { get; set; }

        public long Start { get; set; }

        public float? MaxScore { get; set; }
    }
}