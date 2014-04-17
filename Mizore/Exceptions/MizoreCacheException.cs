using Mizore.CacheHandler;
using System;

namespace Mizore.Exceptions
{
    public class MizoreCacheException : MizoreException
    {
        public MizoreCacheException(ICacheHandler cache, Exception innerException)
            : this(cache, innerException.Message, innerException)
        {
        }

        public MizoreCacheException(ICacheHandler cache, string message, Exception innerException)
            : base(message, innerException)
        {
            CacheHandler = cache;
        }

        public ICacheHandler CacheHandler { get; protected set; }
    }
}