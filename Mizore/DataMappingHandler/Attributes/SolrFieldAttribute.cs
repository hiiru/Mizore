using System;

namespace Mizore.DataMappingHandler.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SolrFieldAttribute : Attribute
    {
        public SolrFieldAttribute()
            : this(null, 1f)
        {
        }

        public SolrFieldAttribute(string field)
            : this(field, 1f)
        {
        }

        public SolrFieldAttribute(float boost)
            : this(null, boost)
        {
        }

        public SolrFieldAttribute(string field, float boost)
        {
            if (field != null)
                Field = field;
            Boost = boost;
        }

        public string Field { get; set; }

        public float Boost { get; set; }

        /// <summary>
        /// This Field is ignored when updating, however when getting the document from solr, the field is loaded.
        /// </summary>
        public bool ReadOnly { get; set; }
    }
}