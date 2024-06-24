using GemsOfWarsMainTypes.SubType;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleControlsLibrary.Converters
{
    public class UnitColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ColorUnits cUnit)
            {
                return ((int)cUnit).ToString();
            }
            return Binding.DoNothing;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string sValue)
            {
                var iValue = System.Convert.ToInt32(sValue);
                return (ColorUnits)iValue;
            }
            return Binding.DoNothing;
        }
    }

    //public class UnitColorConverter : IMultiValueConverter
    //{
    //    private ulong _target;

    //    public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //       if (parameter is Enum && value is Enum)
    //    {
    //        var mask = (ulong) parameter;
    //        _target = (ulong) value;
    //        return ((mask & _target) != 0);
    //    }
    //    }

    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        if (value is bool && parameter is Enum)
    //        {
    //            var mask = (ulong)parameter;
    //            if ((bool)value)
    //            {
    //                _target |= mask;
    //            }
    //            else
    //            {
    //                _target &= ~mask;
    //            }
    //            return _target;
    //        }

    //        return Binding.DoNothing;
    //    }

    //}
}
