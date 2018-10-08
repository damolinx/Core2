using System;
using System.Collections.Generic;

namespace Core2.Extensions
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var index = -1;
            foreach (var item in source)
            {
                index++;
                if (predicate(item))
                {
                    return index;
                }
            }

            return -1;
        }
    }
}
