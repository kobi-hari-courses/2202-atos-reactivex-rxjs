using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxFiltering
{
    public static class EnumerableHelper
    {
        public static IEnumerable<TResult> Map<T, TResult>(
            this IEnumerable<T> @this, 
            Func<T, TResult> projection)
        {
            foreach (var item in @this)
            {
                yield return projection(item);
            }
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> @this, Func<T, bool> condition)
        {
            foreach (var item in @this)
            {
                if (condition(item))
                    yield return item;
            }
        }
    }
}
