using System.Collections.Generic;

namespace ScottplotLayer
{
    internal static class HashSetExtesion
    {
        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                if (item != null)
                {
                    set.Add(item);
                }
            }
        }
    }
}
