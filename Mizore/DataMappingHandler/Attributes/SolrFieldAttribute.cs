using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mizore.DataMappingHandler.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SolrFieldAttribute : Attribute
    {
        public SolrFieldAttribute() : this(null, 1f) { }
        public SolrFieldAttribute(string field) : this(field,1f) { }
        public SolrFieldAttribute(float boost) : this(null,boost) { }
        public SolrFieldAttribute(string field, float boost)
        {
            if (field != null)
                Field = field;
            Boost = boost;
        }

        public string Field { get; set; }
        public float Boost { get; set; }
    }
}
