using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarAnalitics;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsMainTypes.SubType;
using GemsOfWarsWARSTAT.DataContext;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GemsOfWarAnalitics.MainStats;
using GemsOfWarAnalitics.Filter;
using GemsOfWarsWARSTAT.Services;
using System.Diagnostics;


namespace GemsOfWarsWARSTAT.ViewModel.Statistic
{
    public partial class StatisticMainViewModel : BaseVisualViewModel
    {
        private OneDataGridVisibility _dataGridVisibility = new OneDataGridVisibility();
        private RelayCommand _updateUnit;
        private RelayCommand _updateDefence;
        private RelayCommand _updateUser;
        private RelayCommand _InBufMax;
        private RelayCommand _clear;
        private RelayCommand _matrixVictory;
        private RelayCommand _userStatTable;

        private RelayCommand _quantityFilterCommand;
        private RelayCommand _clearWarFlags;
        private RelayCommand _setAllWarFlags;
        private RelayCommand _useGroupCommand;

        private ColorUnits _colorWarDay;
        private ColorUnits _colorUnits;

        private int _quantityFilterValue;
        private bool _quantityFilterChecked;

        private bool _notEmptyValuesChecked;
        private bool _useGroup;

        private List<RealWarState> _realStates;
        private List<Guild> _guilds;
        private List<UserInGuild> _userInGuild;
        private List<Unit> _units;
        private List<WarDay> _warDays;
        private List<War> _wars;
        private List<Defence> _defences;
        private List<User> _users;
        private List<CheckedViewModel<War>> _warDayCalcViewModels;
        private List<CheckedViewModel<int>> _basketDaysCalcViewModels;
        private List<CheckedViewModel<int>> _group1DaysCalcViewModels;
        private List<CheckedViewModel<int>> _group2DaysCalcViewModels;
        private List<CheckedViewModel<int>> _group3DaysCalcViewModels;
        private ObservableCollection<BaseStatItem> _calculationStatistic = new ObservableCollection<BaseStatItem>();
        private ObservableCollection<BaseStatItem> _calculationFiltredStatistic = new ObservableCollection<BaseStatItem>();
        private ObservableCollection<BaseStatItem> _calculationFiltredGroupStatistic = new ObservableCollection<BaseStatItem>();
        private List<WarGuild> _warGuilds;
        private int[] _baskets;
        private bool _useDifference;
        private bool _useBasket;
        private bool _isEnabled;

        public OneDataGridVisibility DataGridVisibility => _dataGridVisibility;

        public StatisticMainViewModel(WarDbContext warDbContext) : base(warDbContext)
        {
            UpdateUnit = new RelayCommand(OnUpdateUnit);
            UpdateDefence = new RelayCommand(OnUpdateDefence);
            UpdateUser = new RelayCommand(OnUpdateUser);
            _InBufMax = new RelayCommand(OnBufMax, OnBufMaxCan);
            FilterCommand = new RelayCommand(UseFilters, OnBufMaxCan);
            MatrixVictory = new RelayCommand(GetMatrixVictory);
            UserStatTable = new RelayCommand(CalcUserUseStatTable);
            ClearWarFlags = new RelayCommand(OnClearFlags, OnAnyFlagsSet);
            SetAllWarFlags = new RelayCommand(OnSetAllFlags, NotAllSet);
            UseGroupCommand = new RelayCommand(OnUseGroup, OnBufMaxCan);
            
            DataGridVisibility.SetOneVisibility(DataGridItemVisibility.StandartDataGrid);
        }

        private bool NotAllSet()
        {
            return !WarCalcViewModels.All(x => x.IsChecked);
        }

        private void OnSetAllFlags()
        {
            OnSetFlags(true);
        }

        private void OnClearFlags()
        {
            OnSetFlags(false);
        }

        private void OnSetFlags(bool set)
        {
            foreach (var viewModelWarFlag in WarCalcViewModels)
            {
                viewModelWarFlag.IsChecked = set;
            }
        }

        private bool OnAnyFlagsSet()
        {
            return WarCalcViewModels.Any(x => x.IsChecked);
        }


        private bool OnBufMaxCan()
        {
            return _calculationStatistic.Count > 0;
        }


        private IEnumerable<Unit> FiltredUnits(List<Unit> units)
        {
            foreach (var item in units)
            {
                if (!ColorUnits.IsUsesColor() || item.ColorUnits.IsContains(ColorUnits))
                {
                    yield return item;
                }
            }
        }

        public RelayCommand UpdateUnit { get => _updateUnit; set => _updateUnit = value; }

        public RelayCommand Clear { get => _clear; set => _clear = value; }
        public ColorUnits ColorWarDay
        {
            get => _colorWarDay;
            set
            {
                _colorWarDay = value;
                RaisePropertyChanged(nameof(ColorWarDay));
            }
        }

        public ObservableCollection<BaseStatItem> CalculationStatistic
        {
            get => _calculationStatistic;
            set
            {
                _calculationStatistic = value;
                _calculationFiltredStatistic.Clear();
                _calculationFiltredStatistic.AddRange(value);

                _quantityFilterChecked = false;
                _notEmptyValuesChecked = false;

                RaisePropertyChanged(nameof(CalculationStatistic));
                RaisePropertyChanged(nameof(CalculationFiltredStatistic));
                RaisePropertyChanged(nameof(QuantityFilterChecked));
                RaisePropertyChanged(nameof(NotEmptyValuesChecked));
            }
        }

        public ObservableCollection<BaseStatItem> CalculationFiltredStatistic
        {
            get => _calculationFiltredStatistic;
            set
            {
                _calculationFiltredStatistic.Clear();
                _calculationFiltredStatistic.AddRange(value);
                _calculationFiltredGroupStatistic.Clear();
                _calculationFiltredGroupStatistic.AddRange(value);
                RaisePropertyChanged(nameof(CalculationFiltredStatistic));
                _quantityFilterChecked = false;
                _notEmptyValuesChecked = false;

                RaisePropertyChanged(nameof(CalculationFiltredStatistic));
                RaisePropertyChanged(nameof(QuantityFilterChecked));
                RaisePropertyChanged(nameof(NotEmptyValuesChecked));
            }
        }

        public RelayCommand InBufMax { get => _InBufMax; set => _InBufMax = value; }

        public ColorUnits ColorUnits
        {
            get => _colorUnits;
            set
            {
                _colorUnits = value;
                RaisePropertyChanged(nameof(ColorUnits));
            }
        }

        public RelayCommand FilterCommand { get => _quantityFilterCommand; set => _quantityFilterCommand = value; }
        public int QuantityFilterValue
        {
            get => _quantityFilterValue;
            set
            {
                if (_quantityFilterValue == value)
                {
                    return;
                }
                _quantityFilterValue = value;
                RaisePropertyChanged(nameof(QuantityFilterValue));
            }
        }

        public bool QuantityFilterChecked
        {
            get => _quantityFilterChecked;
            set
            {
                if (_quantityFilterChecked == value)
                {
                    return;
                }
                _quantityFilterChecked = value;
                RaisePropertyChanged(nameof(QuantityFilterChecked));
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled == value)
                {
                    return;
                }
                _isEnabled = value;
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }

        public bool NotEmptyValuesChecked
        {
            get => _notEmptyValuesChecked;
            set
            {
                if (_notEmptyValuesChecked == value)
                {
                    return;
                }
                _notEmptyValuesChecked = value;
                RaisePropertyChanged(nameof(NotEmptyValuesChecked));
            }
        }
        
        public RelayCommand UserStatTable { get => _userStatTable; set => _userStatTable = value; }
        public RelayCommand UpdateDefence { get => _updateDefence; set => _updateDefence = value; }
        public RelayCommand UpdateUser { get => _updateUser; set => _updateUser = value; }

        public RelayCommand ClearWarFlags { get => _clearWarFlags; set => _clearWarFlags = value; }
        public RelayCommand SetAllWarFlags { get => _setAllWarFlags; set => _setAllWarFlags = value; }

        public RelayCommand UseGroupCommand { get => _useGroupCommand; set => _useGroupCommand = value; }

        public List<CheckedViewModel<War>> WarCalcViewModels
        {
            get => _warDayCalcViewModels;
            set
            {
                _warDayCalcViewModels = value;
                RaisePropertyChanged(nameof(WarCalcViewModels));
            }
        }

        public List<CheckedViewModel<int>> BasketCalcViewModels
        {
            get => _basketDaysCalcViewModels;
            set
            {
                _basketDaysCalcViewModels = value;
                RaisePropertyChanged(nameof(BasketCalcViewModels));
            }
        }

        public List<CheckedViewModel<int>> Group1DaysCalcViewModels
        {
            get => _group1DaysCalcViewModels;
            set
            {
                _group1DaysCalcViewModels = value;
                RaisePropertyChanged(nameof(Group1DaysCalcViewModels));
            }
        }

        public List<CheckedViewModel<int>> Group2DaysCalcViewModels
        {
            get => _group2DaysCalcViewModels;
            set
            {
                _group2DaysCalcViewModels = value;
                RaisePropertyChanged(nameof(Group2DaysCalcViewModels));
            }
        }

        public List<CheckedViewModel<int>> Group3DaysCalcViewModels
        {
            get => _group3DaysCalcViewModels;
            set
            {
                _group3DaysCalcViewModels = value;
                RaisePropertyChanged(nameof(Group3DaysCalcViewModels));
            }
        }

        public bool UseDifference
        {
            get => _useDifference;
            set
            {
                _useDifference = value;
                RaisePropertyChanged(nameof(UseDifference));
            }
        }

        public bool UseBasket
        {
            get => _useBasket;
            set
            {
                _useBasket = value;
                RaisePropertyChanged(nameof(UseBasket));
            }
        }

        public RelayCommand MatrixVictory { get => _matrixVictory; set => _matrixVictory = value; }

        public override VisiblityControls ViewModelType => VisiblityControls.Statistic;

        public bool UseGroup 
        { 
            get => _useGroup;
            set
            {
                _useGroup = value;
                RaisePropertyChanged(nameof(UseGroup));
            }
        }
    }
}
