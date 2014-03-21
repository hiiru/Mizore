using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mizore.util;

namespace Mizore
{
    public static class MizoreExtensions
    {
        public static bool IsNullOrEmpty(this INamedList namedList)
        {
            if (namedList == null) return true;
            return namedList.Count == 0;
        }

        public static T GetOrDefault<T>(this INamedList namedList, string key) where T : class 
        {
            if (namedList.IsNullOrEmpty() || string.IsNullOrWhiteSpace(key)) return null;
            var item = namedList.Get(key);
            if (item == null) return null;
            return item as T;
        }
        public static T GetOrDefaultStruct<T>(this INamedList namedList, string key) where T : struct
        {
            if (namedList.IsNullOrEmpty() || string.IsNullOrWhiteSpace(key)) return default(T);
            var item = namedList.Get(key);
            if (item == null) return default(T);
            try
            {
                return (T) item;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
