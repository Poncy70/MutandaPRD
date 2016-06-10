using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Mutanda.Extensions
{
    public static class IEnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            var result = new ObservableCollection<T>();
            result.AddRange(items);
            return result;
        }
    }
}
