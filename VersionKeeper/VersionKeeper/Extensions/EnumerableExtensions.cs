using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TItem> WhereNotNull<TItem>(this IEnumerable<TItem> source)
            where TItem : class
        {
            return source.Where(p => p != null);
        }

        public static IOrderedEnumerable<TItem> OrderBySelf<TItem>(this IEnumerable<TItem> source)
        {
            return source.OrderBy(p => p);
        }

        public static IOrderedEnumerable<TItem> ThenBySelf<TItem>(this IOrderedEnumerable<TItem> source)
        {
            return source.ThenBy(p => p);
        }
    }
}
