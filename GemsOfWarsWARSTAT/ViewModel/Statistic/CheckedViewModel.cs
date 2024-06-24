using GalaSoft.MvvmLight;
using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsWARSTAT.ViewModel.Statistic
{
    public class CheckedViewModel<T> : ViewModelBase
    {
        private T _checkItem;
        private bool _isChecked = true;
        public CheckedViewModel(T warDay)
        {
            _checkItem = warDay;
        }

        public T CheckItem { get => _checkItem; set => _checkItem = value; }
        public bool IsChecked 
        { 
            get => _isChecked; 
            set 
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    RaisePropertyChanged(nameof(IsChecked));
                }
            }
        }
    }
}
