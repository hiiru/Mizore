using System.Collections.Generic;

namespace Mizore.util
{
    public interface INamedList : INamedList<object> { }

    public interface INamedList<T>
    {
        T Get(int i);

        T Get(string name);

        IList<T> GetAll(string name);

        string GetKey(int i);

        void Add(string name, T obj);

        int Count { get; }
    }
}