using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace GemsOfWarsMainTypes.Extension
{
    public static class IClonableExtension
    {
        public static T Clone<T>(this ICloneable clone)
        {
            return (T)clone.Clone();
        }
    }
}
