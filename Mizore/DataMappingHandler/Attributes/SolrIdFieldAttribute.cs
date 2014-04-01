using System;

namespace Mizore.DataMappingHandler.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SolrIdFieldAttribute : SolrFieldAttribute
    {
        //public SolrIdFieldAttribute(string field = null, float? boost = null) : base (field,boost) {}

        public SolrIdFieldAttribute()
            : base()
        {
        }

        public SolrIdFieldAttribute(string field)
            : base(field)
        {
        }

        public SolrIdFieldAttribute(float boost)
            : base(boost)
        {
        }

        public SolrIdFieldAttribute(string field, float boost)
            : base(field, boost)
        {
        }
    }
}