using System.Collections.Generic;

namespace Mizore.ContentSerializer.Data.Solr
{
    public class SolrDocument
    {
        public SolrDocument(Dictionary<string, SolrInputField> fields = null)
        {
            Fields = fields ?? new Dictionary<string, SolrInputField>();
        }

        public SolrDocument(INamedList namedList)
        {
            Fields = new Dictionary<string, SolrInputField>();
            if (namedList == null)
                return;
            for (int i = 0; i < namedList.Count; i++)
            {
                var key = namedList.GetKey(i);
                var val = namedList.Get(i);
                Fields.Add(key, new SolrInputField(key) { Value = val });
            }
        }

        public Dictionary<string, SolrInputField> Fields { get; protected set; }
    }
}