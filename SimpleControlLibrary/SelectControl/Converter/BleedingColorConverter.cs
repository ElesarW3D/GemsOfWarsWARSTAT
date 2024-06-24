using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SimpleControlLibrary.SelectControl.Converter
{
    public class BleedingColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Color cColor && values[1] is bool bSelected && values[2] is bool bnonSelected)
            {
                if (bSelected || bnonSelected)
                {
                    return cColor;
                }
                var red = cColor.R;
                var green = cColor.G;
                var blue = cColor.B;
                return Color.FromArgb(127,red,green,blue);
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
