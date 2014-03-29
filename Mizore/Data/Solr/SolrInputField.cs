using System.Collections;

namespace Mizore.Data.Solr
{
    public class SolrInputField
    {
        public SolrInputField(string name, object value = null, float boost = 1f)
        {
            Name = name;
            Value = value;
            Boost = boost;
        }

        private object _value;

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                //TODO: check if ICollection could be used here
                if (value is object[])
                {
                    _value = new ArrayList(value as object[]);
                }
                else
                    _value = value;
            }
        }

        public float Boost { get; set; }

        public string Name { get; set; }

        public int Count
        {
            get
            {
                if (_value == null)
                    return 0;
                if (_value is ICollection)
                    return (_value as ICollection).Count;
                return 1;
            }
        }

        public override string ToString()
        {
            if (Boost == 1f)
                return string.Format("{0}={1}", Name, Value);
            return string.Format("{0}({2})={1}", Name, Value, Boost);
        }

        public SolrInputField DeepClone()
        {
            var clone = new SolrInputField(Name);
            clone.Boost = Boost;
            if (Value is ICollection)
                clone.Value = new ArrayList((Value as ICollection));
            else
                clone.Value = Value;
            return clone;
        }
    }
}