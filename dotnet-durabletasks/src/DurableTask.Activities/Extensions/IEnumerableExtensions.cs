using System.Collections.Generic;
using System.Linq;

namespace DurableTask.Activities
{
    static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> ToBatches<T>(this IEnumerable<T> item, int size)
        {
            while (item.Any())
            {
                yield return item.Take(size);
                item = item.Skip(size);
            }
        }
    }
}
