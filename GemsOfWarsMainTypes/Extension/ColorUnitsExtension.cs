using GemsOfWarsMainTypes.SubType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GemsOfWarsMainTypes.Extension
{
    public static class ColorUnitsExtension
    {
        public static bool IsUsesColor(this ColorUnits color)
        {
            return color != ColorUnits.None && color != ColorUnits.All;
        }
        public static IEnumerable<ColorUnits> GetByOneColor(this ColorUnits colorsUnit)
        {
            foreach (var color in Enum.GetValues(typeof(ColorUnits)).OfType<ColorUnits>())
            {
                if (!color.IsUsesColor())
                {
                    continue;
                }

                if ((colorsUnit & color) != 0)
                {
                    yield return color;
                }
            } 
        }

        public static bool IsContains(this ColorUnits checkedColors, ColorUnits findColors)
        {
            foreach (var findColor in findColors.GetByOneColor())
            {
                foreach (var checkedColor in checkedColors.GetByOneColor())
                {
                    if ((checkedColor & findColor) != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        
    }
}
