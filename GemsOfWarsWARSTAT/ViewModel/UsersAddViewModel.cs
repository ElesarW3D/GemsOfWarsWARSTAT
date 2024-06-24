using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsMainTypes.Model;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GemsOfWarsMainTypes.Extension;
using System;
using static GemsOfWarsMainTypes.GlobalConstants;
using GemsOfWarsWARSTAT.Services;
using System.Windows;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class UsersAddViewModel : WarStatsBaseViewModel
    {
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        public UsersAddViewModel(ObservableCollection<User> users,WarDbContext context) : base(context)
        {
            _users = users;
            RatUsers = new RelayCommand(OnRatUsers);
          
        }
       
        public ICommand RatUsers
        {
            get; set;
        }

        private void OnRatUsers()
        {
            Context.Wars.Load();
            var lastWar = Context.Wars.OrderBy(x => x.DateStart).ToArray().Last();

            Context.WarDays.Include(x => x.War).Load();
                

            var warDaysLast = Context.WarDays.Where(x => x.War.Id == lastWar.Id).ToArray();

            var lastDayUsers = warDaysLast
                .Select(x => x.User)
                .Distinct();
            var userInNotAnabious = Context.UsersInGuilds
                .Where(x => x.DateFinish != null)
                .Where(x => x.DateFinish <= lastWar.DateStart)
                .ToArray()
                .Select(x=>x.User);

            var notDropDefenceUsers = Users.Except(lastDayUsers).ToList();
            var ratUsers = notDropDefenceUsers.Except(userInNotAnabious);
            var buffer = ""; 
            var index = 1;
            foreach (var rat in ratUsers)
            {
                buffer += $"{index}. {rat.Name} {Environment.NewLine}";
                index++;
            }
            Clipboard.SetText(buffer);

        }

        public override void Load()
        {
            SetSelectToLast(Users);
        }

        protected override void OnUpdateItem()
        {
            var users = Context.Users.Local.ToArray();
            if (CheckValidation(users, DisplayUser))
            {
                return;
            }

            var user = Context.Users.FirstOrDefault(it => it.Id == SelectionUser.Id);
            if (user != null)
            {
                user.ReadFromItem(SelectionUser);
            }
            else
            {
                Context.Users.Add(SelectionUser);
            }
           
            DoOnUpdateItem();
            RaisePropertyChanged(nameof(SelectionUser.Name));
            RaiseOnChangeItem();
        }

        public User SelectionUser => Selection as User;

        protected override void OnAddItem()
        {
            var newItem = new User()
            {
                Name = GlobalConstants.NonameUnit,
            };

            Selection = newItem;
            Users.Add(newItem);
            DoOnAddItem();
            RaiseOnChangeItem();
        }

        protected override void OnDeleteItem()
        {
            var user = Context.Users.FirstOrDefault(it => it.Id == SelectionUser.Id);

            Users.Remove(SelectionUser);
            Selection = null;
            if (user != null)
            {
                Context.Users.Remove(user);
            }

            DoOnUpdateItem();

            SetSelectToLast(Users);
            RaiseOnChangeItem();
        }

        public override WarStatsModelViewModel Selection 
        { 
            get => base.Selection;
            set
            {
                base.Selection = value;
                RaisePropertyChanged(nameof(SelectionUser.Name));
            }
        }

        public override VisiblityControls ViewModelType => VisiblityControls.UsersAdd;

        private void RaiseOnChangeItem()
        {
            OnChangeItem?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler OnChangeItem;
    }
}
