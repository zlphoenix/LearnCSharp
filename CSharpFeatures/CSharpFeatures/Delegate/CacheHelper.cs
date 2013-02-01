using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.Delegate
{
    //public delegate void CacheGetter(out int data);
    public static class CacheHelper
    {
        public delegate bool CacheGetter<TData>(out TData data);
     

        public static TData Get<TData>(
        CacheGetter<TData> cacheGetter, /* get data from cache */
        Func<TData> sourceGetter, /* get data from source (fairly expensive) */

        Action<TData> cacheSetter /* set the data to cache */)
        {
            TData data;
            if (cacheGetter(out data))
            {
                return data;
            }

            data = sourceGetter();
            cacheSetter(data);

            return data;
        }
        //public event CacheGetter OnCreate;

    }
}
