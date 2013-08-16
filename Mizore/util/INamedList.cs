using System.Collections.Generic;

namespace Mizore.util
{
    public interface INamedList : INamedList<object> { }

    public interface INamedList<T>
    {
        T Get(int i);

        T Get(string name);

        IList<T> GetAll(string name);

        void Add(string name, T obj);
    }
}