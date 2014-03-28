using System.Collections.Generic;

namespace Mizore.Data
{
    public interface INamedList : INamedList<object> { }

    public interface INamedList<T>
    {
        T Get(int i);

        T Get(string key);

        IList<T> GetAll(string key);

        string GetKey(int i);

        void Add(string name, T obj);

        int Count { get; }
    }
}