using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SimpleControlsLibrary.Converters
{
    public class SelectionToUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Unit unit )
            {
                return unit;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Unit unit)
            {
                return unit;
            }
            return Binding.DoNothing;
            
            
        }
    }
}
