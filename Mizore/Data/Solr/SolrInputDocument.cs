using System.Collections.Generic;

namespace Mizore.Data.Solr
{
    public class SolrInputDocument : SolrDocument
    {
        public SolrInputDocument(Dictionary<string, SolrInputField> fields = null) : base(fields: fields)
        {
            DocBoost = 1f;
        }
        
        public List<SolrInputDocument> ChildDocuments { get; set; }

        public float? DocBoost { get; set; }

        public void Clear()
        {
            Fields.Clear();
            ChildDocuments = null;
        }

        public override string ToString()
        {
            if (ChildDocuments == null)
                return string.Format("SolrInputDocument(fields: {0})", Fields.Keys);
            return string.Format("SolrInputDocument(fields: {0}, children: {1})", Fields.Keys, ChildDocuments);
        }
    }
}