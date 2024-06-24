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
    public class UnitsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IList<Unit> collection)
            {
                var list = new List<string>(collection.Count);
                foreach (var item in collection)
                {
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        list.Add("Unnamed");
                    }
                    else
                    {
                        list.Add(item.Name);
                    }

                }

                return list.ToArray();
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
