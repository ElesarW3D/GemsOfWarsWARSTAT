using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SimpleControlsLibrary.Converters
{
    class GetDisplayNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                              Type targetType,
                              object parameter,
                              System.Globalization.CultureInfo culture)
        {
            // I added this because I kept getting DependecyProperty.UnsetValue 
            // Passed in as the program initializes
            if (values[0] as string != null)
            {
                string firstName = (string)values[0];
                string lastName = (string)values[1];
                string fullName = firstName + " " + lastName;
                return fullName;
            }
            return null;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
