using GemsOfWarsMainTypes.SubType;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace SimpleControlsLibrary.Converters
{
    public class ColorDefenceConverterColor : IValueConverter
    {
        private static Dictionary<ColorUnits, Color> _colorsKey = new Dictionary<ColorUnits, Color>()
        {
            {
                ColorUnits.Red,
                Colors.Red
            },
            {
                ColorUnits.Brown,
                Colors.Brown
            },
            {
                ColorUnits.Purple,
                Colors.Purple
            },
            {
                ColorUnits.Yellow,
                Colors.Yellow
            },
            {
                ColorUnits.Green,
                Colors.Green
            },
            {
                ColorUnits.Blue,
                Colors.Blue
            },
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ColorUnits cUnit)
            {
                if (_colorsKey.ContainsKey(cUnit))
                return _colorsKey[cUnit];
            }
            else
            {
                return null;
            }
            return Binding.DoNothing;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color cValue)
            {
                var values = _colorsKey.Values.ToArray();
                var index = Array.IndexOf(values, cValue);
                if (index >= 0)
                {
                    return (ColorUnits)index;
                } 
            }
            else
            {
                return 0;
            }
            return Binding.DoNothing;
        }
    }
}
