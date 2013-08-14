namespace Mizore.CacheHandler
{
    public interface ICacheHandler
    {
        //initialize cache
        void Init();

        bool Contains(string key);

        // gets item from cache
        object Get(string key);

        string GetETag(string key);

        // stores item in cache
        void Put(string key, object obj);

        //clears cache
        void Flush();

        //removes item from cache
        void Remove(string key);
    }
}