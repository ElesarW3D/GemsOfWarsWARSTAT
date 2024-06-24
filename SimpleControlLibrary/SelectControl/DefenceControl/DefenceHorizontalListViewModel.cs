using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleControlLibrary.SelectControl.DefenceControl
{
    public class DefenceHorizontalListViewModel : ViewModelBase
    {
        private BindingList<Defence> _defenceList;
        private int _current;
        private Defence _selection;
        private RelayCommand _nextItem;
        private RelayCommand _previousItem;
        private bool _isEditing;

        public DefenceHorizontalListViewModel(BindingList<Defence> unitsList, Defence selection, int current = 0)
        {
            Defences = unitsList;
            Selection = selection;
            _current = current;
            NextItem = new RelayCommand(OnNextItem, CanExecuteNextItem);
            PreviousItem = new RelayCommand(OnPreviousItem, CanExecutePreviousItem);
        }

        private bool CanExecutePreviousItem()
        {
            return _current > 0;
        }

        private void OnPreviousItem()
        {
            _current--;
            Selection = _defenceList[_current];
            UpdateCommand();
        }

        private void UpdateCommand()
        {
            NextItem.RaiseCanExecuteChanged();
            PreviousItem.RaiseCanExecuteChanged();
        }

        private bool CanExecuteNextItem()
        {
           return _current < _defenceList.Count-1;
        }

        private void OnNextItem()
        {
            _current++;
            Selection = _defenceList[_current];
            UpdateCommand();
        }

        public BindingList<Defence> Defences
        {
            get { return _defenceList; }
            set
            {
                _defenceList = value;
                RaisePropertyChanged(nameof(Defences));
            }
        }

        public int Tag => _current;
        public Defence Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                if (_selection != null)
                {
                    _current = Array.IndexOf(_defenceList.ToArray(), value);
                    if (_current < 0)
                    {
                        Debug.Fail("What is not contain?");
                        _current = 0;
                    }
                }
                RaisePropertyChanged(nameof(Selection.DisplayName));
                RaisePropertyChanged(nameof(Selection));
            }
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                RaisePropertyChanged(nameof(IsEditing));
            }
        }

        public RelayCommand NextItem { get => _nextItem; set => _nextItem = value; }
        public RelayCommand PreviousItem { get => _previousItem; set => _previousItem = value; }
    }
}
