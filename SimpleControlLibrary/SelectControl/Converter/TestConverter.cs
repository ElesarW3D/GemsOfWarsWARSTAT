using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using SimpleControlLibrary.SelectControl.DefenceControl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SimpleControlsLibrary.Converters
{
   
    public class TestConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsNotNull())
            {
                return value;

            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsNotNull())
            {
                return value;

            }
            return Binding.DoNothing;
        }
    }
    public class TestConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsNotNull())
            {
                return value;

            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsNotNull())
            {
                return value;

            }
            return Binding.DoNothing;
        }
    }
}
