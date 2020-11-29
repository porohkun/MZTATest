using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, TSource second)
        {
            foreach (var entry in first)
                yield return entry;
            yield return second;
        }
    }
}
