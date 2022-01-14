using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<TItem> ToObservableCollection<TItem>(this IEnumerable<TItem> sequence)
        {
            return new ObservableCollection<TItem>(sequence);
        }
    }
}
