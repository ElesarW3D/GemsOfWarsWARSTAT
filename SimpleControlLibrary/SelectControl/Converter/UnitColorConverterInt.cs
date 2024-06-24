using GemsOfWarsMainTypes.SubType;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleControlsLibrary.Converters
{
    public class UnitColorConverterInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ColorUnits cUnit)
            {
                return ((int)cUnit);
            }
            return Binding.DoNothing;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int iValue)
            {
                return (ColorUnits)iValue;
            }
            return Binding.DoNothing;
        }
    }
}
