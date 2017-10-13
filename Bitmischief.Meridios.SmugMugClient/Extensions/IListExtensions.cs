using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmischief.Meridios.SmugMug.Extensions
{
    public static class IListExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> enumerable)
        {
            foreach (var cur in enumerable)
            {
                list.Add(cur);
            }
        }
    }
}
