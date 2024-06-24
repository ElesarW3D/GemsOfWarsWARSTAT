using System.Collections.Generic;
using System.Linq;

namespace GemsOfWarsMainTypes.Extension
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object obj) => obj == null;

        public static bool IsNotNull(this object obj) => obj != null;
        public static IEnumerable<T> ToEnumirable<T>(this T obj) => new T[] { obj };
    }
}
