using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DColor = System.Drawing.Color;
using System.Windows.Media;
using GemsOfWarsMainTypes.SubType;

namespace GemsOfWarsMainTypes.Extension
{
    public static class ColorExtension
    {
        public static DColor ToDColor(this Color color)
        {
            var a = color.A;
            var b = color.B;
            var r = color.R;
            var g = color.G;
            return DColor.FromArgb(a,r, g, b);
        }

        public static Color ToColor(this DColor color)
        {
            var a = color.A;
            var b = color.B;
            var r = color.R;
            var g = color.G;
            return Color.FromArgb(a, r, g, b);
        }

        public static DColor GetLightColor(this ColorUnits colorUnits)
        {
            switch (colorUnits)
            {
                case ColorUnits.Red:
                    return DColor.Red;
                case ColorUnits.Brown:
                    return DColor.Brown;
                case ColorUnits.Purple:
                    return DColor.Violet;
                case ColorUnits.Yellow:
                    return DColor.Yellow;

                case ColorUnits.Green:
                    return DColor.Green;
                case ColorUnits.Blue:
                    return DColor.Blue;


            }
            return DColor.Black;
        }
        public static DColor GetDarkColor(this ColorUnits colorUnits)
        {
            return GetLightColor(colorUnits);
        }
    }
}
