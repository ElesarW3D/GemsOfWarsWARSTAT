using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleControlLibrary.SelectControl.DefenceControl
{
    public class DefenceSelecterViewModel : ViewModelBase
    {
        private ObservableCollection<Unit> _unitsList;
        private int _tag;
        private Unit _selection;
        private bool _isEditing;
        protected ICommand _startDragCommand;
        protected ICommand _completeDragCommand;

        public DefenceSelecterViewModel(ObservableCollection<Unit> unitsList, Unit selection, int tag)
        {
            Units = unitsList;
            Selection = selection;
            _tag = tag;
            StartDragCommand = new RelayCommand<object>(OnStartDrag);
            CompleteDragCommand = new RelayCommand<object>(OnCompleteDrag);
        }
        private void OnCompleteDrag(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnStartDrag(object obj)
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<Unit> Units
        {
            get { return _unitsList; }
            set
            {
                _unitsList = value;
                RaisePropertyChanged(nameof(Units));
            }
        }

        public int Tag => _tag;
        public Unit Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
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
        public ICommand StartDragCommand { get => _startDragCommand; set => _startDragCommand = value; }
        public ICommand CompleteDragCommand { get => _completeDragCommand; set => _completeDragCommand = value; }

    }
}
