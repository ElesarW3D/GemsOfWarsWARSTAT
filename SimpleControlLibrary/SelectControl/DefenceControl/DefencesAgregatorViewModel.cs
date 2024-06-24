using GalaSoft.MvvmLight;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleControlLibrary.SelectControl.DefenceControl
{
    public class DefencesAgregatorViewModel : ViewModelBase
    {
        private ObservableCollection<DefenceSelecterViewModel> _viewModels = new ObservableCollection<DefenceSelecterViewModel>();
        private ObservableCollection<Unit> _unitsList;
        private IDefences _defences;
        private bool _isEditing;

        public ObservableCollection<DefenceSelecterViewModel> ViewModels
        {
            get { return _viewModels; }
            set
            {
                _viewModels = value;
                RaisePropertyChanged(nameof(ViewModels));
            }
        }

        public bool IsEditing
        { 
            get => _isEditing; 
            set 
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                    foreach (var item in _viewModels)
                    {
                        item.IsEditing = value;
                    }
                }
            }
            
        }

        public ObservableCollection<Unit> UnitsList 
        { 
            get => _unitsList;
            set
            {
                if (value != _unitsList)
                {
                    _unitsList = value;
                    RaisePropertyChanged(nameof(UnitsList));
                }
            }
        }

        public void InitReactionControl()
        {
            _viewModels.Clear();
            _viewModels.Add(CreateViewModel(_defences.Units1, tag: 0, DefenceSelectViewModel_PropertyChanged));
            _viewModels.Add(CreateViewModel(_defences.Units2, tag: 1, DefenceSelectViewModel_PropertyChanged));
            _viewModels.Add(CreateViewModel(_defences.Units3, tag: 2, DefenceSelectViewModel_PropertyChanged));
            _viewModels.Add(CreateViewModel(_defences.Units4, tag: 3, DefenceSelectViewModel_PropertyChanged));
        }

        public void InitUnitsProperty(ObservableCollection<Unit> unitsList)
        {
            UnitsList = unitsList;
            foreach (var item in ViewModels)
            {
                item.Units = UnitsList;
            }
        }

        public void SetNewIDefences(IDefences newSelectedItems)
        {
            _defences = newSelectedItems;
            if (_viewModels.Any())
            {
                ViewModels[0].Selection = newSelectedItems.Units1;
                ViewModels[1].Selection = newSelectedItems.Units2;
                ViewModels[2].Selection = newSelectedItems.Units3;
                ViewModels[3].Selection = newSelectedItems.Units4;
            }
        }

        private void DefenceSelectViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is DefenceSelecterViewModel defenceSelectViewModel
                && e.PropertyName == nameof(defenceSelectViewModel.Selection)
                && defenceSelectViewModel.Selection.IsNotNull())
            {
                switch (defenceSelectViewModel.Tag)
                {
                    case 0:
                        _defences.Units1 = defenceSelectViewModel.Selection;

                        break;
                    case 1:
                        _defences.Units2 = defenceSelectViewModel.Selection;

                        break;
                    case 2:
                        _defences.Units3 = defenceSelectViewModel.Selection;

                        break;
                    case 3:
                        _defences.Units4 = defenceSelectViewModel.Selection;

                        break;
                    default:
                        break;
                }
                //RaisePropertyChanged(nameof(Selection));
                //Context.SaveChanges();
            }
        }

        private DefenceSelecterViewModel CreateViewModel(Unit select, int tag,PropertyChangedEventHandler reaction)
        {
            var defenceSelectViewModel = new DefenceSelecterViewModel(
                            _unitsList,
                            select,
                            tag);
            defenceSelectViewModel.IsEditing = IsEditing;

            defenceSelectViewModel.PropertyChanged += reaction;
            return defenceSelectViewModel;
        }
    }
}
