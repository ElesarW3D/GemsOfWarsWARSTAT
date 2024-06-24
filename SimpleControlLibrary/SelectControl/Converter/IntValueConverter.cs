using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleControlsLibrary.Converters
{
    public class IntValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue.ToString();
            }
            return Binding.DoNothing;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string sValue)
            {
                if (int.TryParse(sValue, out int intValue))
                {
                    return intValue;
                }
            }
            return Binding.DoNothing;
        }
    }
}
