using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsWARSTAT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static GemsOfWarsMainTypes.GlobalConstants;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class UsersInGuildAddViewModel : WarStatsBaseViewModel
    {
        private UserInGuild _selectUserInguild;
        private ObservableCollection<UserInGuild> _usersInGuilds;
        private ObservableCollection<Guild> _guilds;
        private ObservableCollection<User> _users;

        private bool _isChange = false;
        private DateTime _lastWar;
        private Guild _anabios;
        private List<NotifyCollectionChangedEventArgs> _itemsChange = new List<NotifyCollectionChangedEventArgs>();
        public UsersInGuildAddViewModel(
            ObservableCollection<Guild> guilds,
            ObservableCollection<User> users,
            WarDbContext context) : base(context)
        {
            _users = users;
            _guilds = guilds;
            SaveGrid = new RelayCommand(OnSaveGrid, CanSaveGrid);
            CellEditEndingCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(OnCellEditEnding);
        }

        public ICommand SaveGrid
        {
            get; set;
        }

        public ICommand CellEditEndingCommand { get; set; }
        public UserInGuild SelectItem
        {
            get => _selectUserInguild;

            set
            {
                if (SelectItem != value)
                {
                    _selectUserInguild = value;
                    RaisePropertyChanged(nameof(SelectItem));
                }
            }
        }
        private void OnCellEditEnding(DataGridCellEditEndingEventArgs obj)
        {
            var select = SelectItem;
            var index = UsersInGuilds.IndexOf(select);
            if (index != -1)
            {
                UsersInGuilds[index] = select;
            }
        }

        private bool CanSaveGrid()
        {
            return _isChange;
        }

        private void OnSaveGrid()
        {
            var addItems = new Dictionary<int, UserInGuild>();
            foreach (var item in _itemsChange)
            {
                if (item.Action == NotifyCollectionChangedAction.Add)
                {
                    var addItem = UsersInGuilds.ElementAt(item.NewStartingIndex);
                    if (addItem != null)
                    {
                        AddUser(addItem);
                        AddGuild(addItem);
                        var realAdd = Context.UsersInGuilds.Add(addItem);
                        addItems.Add(item.NewStartingIndex, realAdd);
                    }
                }
                if (item.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var removeItem in item.OldItems)
                    {
                        if (removeItem is UserInGuild userInGuild)
                        {
                            Debug.Assert(userInGuild.Id > 0);
                            var removeItemGuild = Context.UsersInGuilds.FirstOrDefault(x => x.Id == userInGuild.Id);
                            if (removeItemGuild != null)
                            {
                                Context.UsersInGuilds.Remove(removeItemGuild);
                            }

                        }
                    }
                }
                if (item.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (var replacement in item.NewItems)
                    {
                        if (replacement is UserInGuild userInGuild)
                        {
                            if (userInGuild.Id == 0)
                            {
                                continue;
                            }
                            Debug.Assert(userInGuild.Id > 0);
                            var removeItemGuild = Context.UsersInGuilds.FirstOrDefault(x => x.Id == userInGuild.Id);
                            if (removeItemGuild != null)
                            {
                                removeItemGuild.ReadFromItem(userInGuild);
                            }

                        }
                    }
                }
            }
            Context.SaveChanges();
            foreach (var realAdd in addItems)
            {
                UsersInGuilds[realAdd.Key] = realAdd.Value;
            }
            _itemsChange.Clear();
            _isChange = false;
        }

        private void AddGuild(UserInGuild addItem)
        {
            var addNameGuild = addItem.Guild.Name;
            var guild = Context.Guilds.FirstOrDefault(it => it.Name == addNameGuild);
            if (guild != null)
            {
                addItem.Guild = guild;
            }
            else
            {
                addItem.Guild = Context.Guilds.Add(addItem.Guild);
            }
        }

        private void AddUser(UserInGuild addItem)
        {
            var addName = addItem.User.Name;
            var user = Context.Users.FirstOrDefault(it => it.Name == addName);
            if (user != null)
            {
                addItem.User = user;
            }
            else
            {
                addItem.User = Context.Users.Add(addItem.User);
            }
        }
        public ObservableCollection<UserInGuild> UsersInGuilds
        {
            get { return _usersInGuilds; }
            set
            {
                _usersInGuilds = value;
                RaisePropertyChanged(nameof(UsersInGuilds));
            }
        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        public ObservableCollection<Guild> Guilds
        {
            get { return _guilds; }
            set
            {
                _guilds = value;
                RaisePropertyChanged(nameof(Guilds));
            }
        }

        public override VisiblityControls ViewModelType => VisiblityControls.UnitsAdd;

        public override void Load()
        {
            Context.UsersInGuilds.Include(x => x.User).Include(x => x.Guild).Load();

            UsersInGuilds = Context.UsersInGuilds.Local.OrderBy(x=>x.User.Name).ToArray().ToObservableCollection();
            UsersInGuilds.CollectionChanged += ItemChange;
            _anabios = Guilds.First();

            _isChange = false;
            _itemsChange.Clear();
            _lastWar = Context.Wars.Select(x => x.DateStart).OrderByDescending(x => x).FirstOrDefault();
            Debug.Assert(_lastWar != null);
        }


        private void ItemChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            _isChange = true;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var newItems = e.NewItems;
                foreach (var item in newItems)
                {
                    if (item is UserInGuild userInGuild)
                    {
                        userInGuild.DateStart = _lastWar;
                        userInGuild.Guild = _anabios;
                        userInGuild.User = new User()
                        {
                            Name = NonameUnit
                        };
                    }
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var newDeleteItems = _itemsChange.Where(it => it.NewStartingIndex == e.OldStartingIndex && it.Action == NotifyCollectionChangedAction.Add).ToArray();
                if (newDeleteItems.Any())
                {
                    foreach (var newDeleteItem in newDeleteItems)
                    {
                        _itemsChange.Remove(newDeleteItem);
                    }
                    return;
                }

            }
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                var replaceItem = _itemsChange.Where(it => it.NewStartingIndex == e.OldStartingIndex && it.Action == NotifyCollectionChangedAction.Replace).ToArray();
                if (replaceItem.Any())
                {
                    foreach (var newDeleteItem in replaceItem)
                    {
                        _itemsChange.Remove(newDeleteItem);
                    }
                    return;
                }
            }
            _itemsChange.Add(e);
        }

        protected override void OnAddItem()
        {
            throw new NotImplementedException();
        }

        protected override void OnDeleteItem()
        {
            throw new NotImplementedException();
        }

        protected override void OnUpdateItem()
        {
            throw new NotImplementedException();
        }
    }
}
