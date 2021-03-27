using System.Collections.Generic;

namespace WebApplication.Common.util
{
    public class SimpleCacheHelper<T>
    {
        private static Dictionary<string, T> cacheDict;
        static SimpleCacheHelper()
        {
            cacheDict=new Dictionary<string, T>();
        }

        public static void AddCache(string key, T value)
        {
            if (cacheDict==null)
            {
                cacheDict=new Dictionary<string, T>();
            }
            else
            {
                if (cacheDict.ContainsKey(key))
                {
                    cacheDict[key] = value;
                    return;
                }
            }
            cacheDict.Add(key,value);
        }

        public static T GetCache(string key)
        {
            T value = default(T);
            if (cacheDict!=null && cacheDict.ContainsKey(key))
            {
                cacheDict.TryGetValue(key, out value);
            }

            return value;
        }
    }
}