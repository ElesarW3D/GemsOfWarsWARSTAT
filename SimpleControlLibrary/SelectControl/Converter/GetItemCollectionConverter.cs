using GemsOfWarsMainTypes.Extension;
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
   
    public class GetItemCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DefencesAgregatorViewModel agregator && parameter is int index)
            {
                if (agregator.ViewModels.IsNotNull() &&
                    agregator.ViewModels.Count > index )
                {
                   return agregator.ViewModels[index];
                }

            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
