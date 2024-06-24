using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows;
using static GemsOfWarsMainTypes.Extension.EnumExtension;

namespace GemsOfWarsWARSTAT.Services
{

    public class VisibilityOneEnumItem<T> : ViewModelBase where T : struct, IConvertible
    {
        protected Dictionary<T, Visibility> _controls;
        
        public VisibilityOneEnumItem()
        {
            _controls = new Dictionary<T, Visibility>();
            foreach (var visiblityControl in GetEnumValues<T>())
            {
                _controls.Add(visiblityControl, Visibility.Hidden);
                RaiseFor(visiblityControl);
            }
        }

        private void RaiseFor(T visiblityControl)
        {
            var property = visiblityControl.ToString() + "Visibility";
            RaisePropertyChanged(property);
        }

        public void SetOneVisibility(T control)
        {
            foreach (var value in GetEnumValues<T>())
            {
                var saveValue = _controls[value];
                var newValue = Visibility.Hidden;
                if (value.Equals(control))
                {
                    newValue = Visibility.Visible;
                }
                if (saveValue != newValue)
                {
                    _controls[value] = newValue;
                    RaiseFor(value);
                }
            }
        }
    }
}
