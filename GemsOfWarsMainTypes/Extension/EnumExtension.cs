using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsMainTypes.Extension
{
    public static class EnumExtension
    {
        //
        public static IEnumerable<TEnum> GetEnumValues<TEnum>() where TEnum : struct, IConvertible
        {
            return Enum.GetValues(typeof(TEnum)).OfType<TEnum>();
        }
    }
}
