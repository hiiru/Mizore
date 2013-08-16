using System.Collections.Generic;

namespace Mizore.util
{
    public class NamedList : NamedList<object>, INamedList { }

    public class NamedList<T> : INamedList<T>
    {
        //TODO: Rebuild a NamedList in c# like lucene's NamedList

        /*
        http://lucene.apache.org/solr/3_6_1/org/apache/solr/common/util/NamedList.html
        A simple container class for modeling an ordered list of name/value pairs.

        Unlike Maps:

            Names may be repeated
            Order of elements is maintained
            Elements may be accessed by numeric index
            Names and Values can both be null

        A NamedList provides fast access by element number, but not by name.

        When a NamedList is serialized, order is considered more important than access by key,
        so ResponseWriters that output to a format such as JSON will normally choose a data structure
        that allows order to be easily preserved in various clients (i.e. not a straight map).
        If access by key is more important, see SimpleOrderedMap, or simply use a regular Map
        */

        private readonly List<T> values = new List<T>();
        private readonly Dictionary<string, List<int>> indexLookup = new Dictionary<string, List<int>>();

        public T Get(int i)
        {
            //values is always filled from 0 to n, so count has to be bigger then i
            return values.Count > i ? values[i] : default(T);
        }

        public T Get(string name)
        {
            return indexLookup.ContainsKey(name) ? Get((int)indexLookup[name][0]) : default(T);
        }

        public IList<T> GetAll(string name)
        {
            if (indexLookup.ContainsKey(name))
            {
                var list = new List<T>(indexLookup[name].Count);
                foreach (int i in indexLookup[name])
                {
                    list.Add(Get(i));
                }
                //for (int i = 0; i < indexLookup[name].Count; i++)
                //{
                //    list.Add(Get(indexLookup[name][i]));
                //}
                return list;
            }
            return null;
        }

        public void Add(string name, T obj)
        {
            List<int> list;
            if (!indexLookup.TryGetValue(name, out list))
                indexLookup[name] = list = new List<int>();

            //if (!indexLookup.ContainsKey(name)) indexLookup.Add(name,new List<int>());
            list.Add(values.Count);
            values.Add(obj);
        }
    }
}