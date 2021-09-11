using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
    public static class LinqExtensions
    {
        public static bool In<T>(this T source, params T[] list) => list.Contains(source);

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}