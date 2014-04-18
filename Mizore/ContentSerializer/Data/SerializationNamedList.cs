using System.Collections.Generic;

namespace Mizore.ContentSerializer.Data
{
    /// <summary>
    /// This NamedList is optimized for fast write, access by Key is not possible!
    /// </summary>
    public class SerializationNamedList : SerializationNamedList<object>, INamedList { }

    /// <summary>
    /// This NamedList is optimized for fast write, access by Key is not possible!
    /// </summary>
    public class SerializationNamedList<T> : List<T>, INamedList<T>
    {
        protected List<string> Keys = new List<string>();

        public T Get(int i)
        {
            return this[i];
        }

        public T Get(string key)
        {
            return Keys.Contains(key) ? this[Keys.IndexOf(key)] : default(T);
        }

        public IList<T> GetAll(string key)
        {
            var list = new List<T>();
            var index = Keys.IndexOf(key);
            do
            {
                list.Add(this[index]);
                index = Keys.IndexOf(key, ++index);
            } while (index != -1);
            return list;
        }

        public string GetKey(int i)
        {
            return Keys[i];
        }

        public void Add(string name, T obj)
        {
            Add(obj);
            Keys.Add(name);
        }
    }
}