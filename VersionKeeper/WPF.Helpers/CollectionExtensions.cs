using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Helpers
{
    public static class CollectionExtensions
    {
        public static void AddRange<TItem>(this ICollection<TItem> collection, IEnumerable<TItem> source)
        {
            foreach (var item in source)
                collection.Add(item);
        }
    }
}
