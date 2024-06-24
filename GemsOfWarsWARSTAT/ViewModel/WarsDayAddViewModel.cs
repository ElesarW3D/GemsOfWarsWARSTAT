using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsMainTypes.Model;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using SimpleControlLibrary.SelectControl.DefenceControl;
using GemsOfWarsMainTypes.SubType;
using System;
using System.Collections.ObjectModel;
using GemsOfWarsMainTypes.Extension;
using static GemsOfWarsMainTypes.GlobalConstants;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;
using GemsOfWarsWARSTAT.Services;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class WarsDayAddViewModel : WarStatsBaseViewModel
    {
        protected ObservableCollection<User> _userList;
        protected ObservableCollection<WarDay> _warDaysList;
        protected ObservableCollection<War> _warsList;
        protected ObservableCollection<Defence> _defencesList;
        protected ObservableCollection<WarDay> _savedWarDays;
        protected ObservableCollection<Unit> _units;
        private ObservableCollection<HeroClass> _heroClases;
       
        protected RelayCommand _cloneItem;
        protected RelayCommand _addUserLostFocus;
        protected RelayCommand _clearFilter;
        protected ICommand _startDragCommand;
        protected ICommand _completeDragCommand;

        protected string _filterName;
        protected string _userSearch;
        protected DefencesAgregatorViewModel _agregatorViewModel;
        protected bool isInitDefencesAgregatorViewModels = false;

        public ObservableCollection<HeroClass> HeroClasses
        {
            get { return _heroClases; }
            set
            {
                _heroClases = value;
                RaisePropertyChanged(nameof(HeroClasses));
            }
        }

        public ObservableCollection<User> Users
        {
            get { return _userList; }
            set
            {
                _userList = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        public ObservableCollection<Defence> Defences
        {
            get { return _defencesList; }
            set
            {
                _defencesList = value;
                RaisePropertyChanged(nameof(Defences));
            }
        }

        public ObservableCollection<WarDay> WarDays
        {
            get { return _warDaysList; }
            set
            {
                _warDaysList = value;
                RaisePropertyChanged(nameof(WarDays));
            }
        }
        public ObservableCollection<War> Wars
        {
            get { return _warsList; }
            set
            {
                _warsList = value;
                RaisePropertyChanged(nameof(Wars));
            }
        }

        public WarsDayAddViewModel(WarDbContext context):base(context)
        {
            CloneItem = new RelayCommand(OnCloneItem, OnCloneCanExecute);
            ClearFilter = new RelayCommand(OnClearFilter, OnClearFilterCanExecute);
            AddUserLostFocus = new RelayCommand(OnAddUserLostFocus);
            AgregatorViewModel = new DefencesAgregatorViewModel();
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

        private void OnAddUserLostFocus()
        {
            if (string.IsNullOrEmpty(TextSearch))
            {
                return;
            }
            var user = Context.Users.FirstOrDefault(x => x.Name == TextSearch);
            if (user != null)
            {
                if (SelectionWarDay.User != user)
                {
                    SelectionWarDay.User = user;
                }
                return ;
            }
            var message = GlobalConstants.Messages.AddUser.Args(TextSearch);
            var result = MessageBox.Show(message,"Добавить пользователя", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var addUser = new User();
                addUser.Name = TextSearch;
                Context.Users.Add(addUser);
                Context.SaveChanges();
                SelectionWarDay.User = addUser;
            }
        }

        private void OnClearFilter()
        {
            FilterName = "";
        }

        private bool OnClearFilterCanExecute()
        {
            return !string.IsNullOrEmpty(_filterName);
        }

        private bool OnCloneCanExecute()
        {
            return Selection!=null && !IsEditing;
        }

        private void OnCloneItem()
        {
            var dayWar = new WarDay()
            {
                ColorDay = SelectionWarDay.ColorDay,        
                Losses = SelectionWarDay.Losses,
                User = SelectionWarDay.User,
                Victories = SelectionWarDay.Victories,
                War = Wars.Last(),
            };

            dayWar.Defence = new Defence();
            dayWar.Defence.Units1 = SelectionWarDay.Units1;
            dayWar.Defence.Units2 = SelectionWarDay.Units2;
            dayWar.Defence.Units3 = SelectionWarDay.Units3;
            dayWar.Defence.Units4 = SelectionWarDay.Units4;
            
            Selection = dayWar;
            WarDays.Add(dayWar);
            IsEditing = true;
        }

        public override void Load()
        {
            Context.Wars.Load();
            Wars = Context.Wars.Include(x=>x.MapColor)
                .OrderBy(x => x.DateStart).ToObservableCollection();

            Users = Context.Users.OrderBy(x => x.Name).ToObservableCollection();

            Context.HeroClasses.Load();
            HeroClasses = Context.HeroClasses.Local.ToObservableCollection();
            
            Context.Units.Load();
            _units = Context.Units.Local.ToArray().OrderBy(x => x.DisplayName).ToObservableCollection();

            Context.WarDays.Load();
            WarDays = Context.WarDays.Local
                .OrderBy(x => x.War.DateStart)
                .ThenBy(x => x.User.Name)
                .ThenBy(x => x.ColorDay)
                .ToObservableCollection();

            _savedWarDays = WarDays.ToObservableCollection();

            Context.Defences.Load();
            Context.Defences
               .Include(db => db.Units1)
               .Include(db => db.Units2)
               .Include(db => db.Units3)
               .Include(db => db.Units4)
               .Include(db => db.HeroClass)
               .Load();
            Defences = Context.Defences.Local.ToArray()
               .ToObservableCollection();
            OnUpdateFilter();
            InitSelection();
        }

        protected void InitSelection()
        {
            var selectedValue = WarDays.FirstOrDefault();
            AgregatorViewModel.SetNewIDefences(selectedValue);
            if (!isInitDefencesAgregatorViewModels)
            {
                AgregatorViewModel.InitReactionControl();
                isInitDefencesAgregatorViewModels = true;
            }
            AgregatorViewModel.InitUnitsProperty(_units);
            if (_userList.Any())
            {
                Selection = selectedValue;
            }
        }

        protected override void OnDeleteItem()
        {
            var warDay = Context.WarDays.FirstOrDefault(it => it.Id == Selection.Id);

            WarDays.Remove(SelectionWarDay);
            Selection = null;
            if (warDay != null)
            {
                Context.WarDays.Remove(warDay);
            }

            DoOnUpdateItem();

            if (WarDays.Any())
            {
                Selection = WarDays.Last();
            }
        }

        protected override void OnUpdateItem()
        {
            var warsDays = Context.WarDays.Local.ToArray();
            if (CheckValidation(warsDays, DisplayWarDay))
            {
                return;
            }

            var addDefence = SelectionWarDay.Defence;
            var defence = Defences.FirstOrDefault(it => it.IsEqualsWithoutCode(addDefence));
            if (defence.IsNull())
            {
                var message = Messages.AddDefence.Args(addDefence.DisplayName);
                var result = MessageBox.Show(message, "Добавить защиту", MessageBoxButton.OKCancel);
                if (result != MessageBoxResult.OK)
                {
                    return;
                }
                Context.Defences.Add(addDefence);
                Context.SaveChanges();
                SelectionWarDay.Defence = addDefence;
            }
            else
            {
                SelectionWarDay.Defence = defence;
            }

            var adduser = SelectionWarDay.User;
            var user = Context.Users.FirstOrDefault(it => it.Name == adduser.Name);
            if (user.IsNull())
            {
                Context.Users.Add(adduser);
            }
            else
            {
                SelectionWarDay.User = user;
            }

            var warDay = Context.WarDays.FirstOrDefault(it => it.Id == SelectionWarDay.Id);
            if (warDay == null)
            {
                Context.WarDays.Add(SelectionWarDay);
            }
            CheckDefence();
            DoOnUpdateItem();
        }

        protected void CheckDefence()
        {
            var defence = SelectionWarDay.Defence;
            var state = Context.Entry(defence).State;
            if (state == EntityState.Modified)
            {
                Debug.Assert(false);
                throw new InvalidOperationException();
            }
        }

        protected override void OnAddItem()
        {
            var dayWar = new WarDay()
            {
                ColorDay = ColorUnits.Red,
                User = Users.First(),
                Losses = 0,
                Victories = 0,
                War = Wars.Last(),
            };
            dayWar.Defence = new Defence();
            Selection = dayWar;
            WarDays.Add(dayWar);
        }

        public override WarStatsModelViewModel Selection
        {
            get { return _selection; }
            set
            {
                base.Selection = value;
                CloneItem.RaiseCanExecuteChanged();
                if (AgregatorViewModel.IsNotNull() && SelectionWarDay.IsNotNull())
                {
                    AgregatorViewModel.SetNewIDefences(SelectionWarDay);
                    RaisePropertyChanged(nameof(AgregatorViewModel));
                }
                RaisePropertyChanged(nameof(Selection));
                RaisePropertyChanged(nameof(SelectionWarDay));
            }
        }

        public WarDay SelectionWarDay => Selection as WarDay;

        public RelayCommand CloneItem 
        { 
            get => _cloneItem; 
            set => _cloneItem = value; 
        }

        public string FilterName 
        { 
            get => _filterName;
            set
            {
                if (_filterName != value)
                {
                    _filterName = value;
                    RaisePropertyChanged(nameof(FilterName));
                    OnUpdateFilter();
                }
            }
        }

        public RelayCommand ClearFilter
        {
            get => _clearFilter;
            set 
            { 
                _clearFilter = value; 
            }
        }

        public RelayCommand AddUserLostFocus { get => _addUserLostFocus; set => _addUserLostFocus = value; }
        public string TextSearch 
        { 
            get => _userSearch;
            set
            {
                if (value != _userSearch)
                {
                    _userSearch = value;
                    RaisePropertyChanged(nameof(TextSearch));
                }
            }
        }

        public DefencesAgregatorViewModel AgregatorViewModel 
        { 
            get => _agregatorViewModel;
            set
            {
                if (_agregatorViewModel != value)
                {
                    _agregatorViewModel = value;
                    RaisePropertyChanged(nameof(AgregatorViewModel));
                }
            }
        }

        private void OnUpdateFilter()
        {
            WarDays.Clear();
            if (string.IsNullOrEmpty(_filterName))
            {
                WarDays.AddRange(_savedWarDays);
            }
            else
            {
                var filteredItems = _savedWarDays.Where(x => x.User.Name.ToLower().IndexOf(_filterName.ToLower())>=0);
                if (filteredItems.Any())
                {
                    Selection = filteredItems.Last();
                    WarDays.AddRange(filteredItems);
                }
            }
        }

        public override bool IsEditing
        {
            get => base.IsEditing;
            set
            {
                if (AgregatorViewModel.IsNotNull())
                {
                    AgregatorViewModel.IsEditing = value;
                }

                base.IsEditing = value;
            }
        }

        public override VisiblityControls ViewModelType => VisiblityControls.WarDaysAdd;

        public ICommand StartDragCommand { get => _startDragCommand; set => _startDragCommand = value; }
        public ICommand CompleteDragCommand { get => _completeDragCommand; set => _completeDragCommand = value; }
    }
}
