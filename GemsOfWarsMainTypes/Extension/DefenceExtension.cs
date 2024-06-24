using GemsOfWarsMainTypes.Model;
using GemsOfWarsMainTypes.Model.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GemsOfWarsMainTypes.Extension
{
    public static class DefenceExtension
    {
        public static IEnumerable<Defence> OrderByUnits(this IEnumerable<Defence> defences)
        {
            return defences.OrderBy(x => x.Units1.DisplayName)
                .ThenBy(x => x.Units2.DisplayName)
                .ThenBy(x => x.Units3.DisplayName)
                .ThenBy(x => x.Units4.DisplayName);
        }
    }

    public static class IDisplayNameExtension
    {
        public static IEnumerable<T> OrderByDisplayName<T>(this IEnumerable<T> items) where T: IDisplayName
        {
            return items.OrderBy(x => x.DisplayName);
        }
    }
}
