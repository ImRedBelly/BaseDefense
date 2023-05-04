using System.Collections.Generic;

namespace Core.Utils
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue def = default)
            => dict.ContainsKey(key) ? dict[key] : def;

        public static TValue Ensure<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (dict.ContainsKey(key)) return dict[key];

            dict[key] = new TValue();

            return dict[key];
        }
    }
}