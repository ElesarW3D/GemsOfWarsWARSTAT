using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsWARSTAT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GemsOfWarsWARSTAT.ViewModel
{
    public class MainUserInGuildAddViewModel : BaseVisualViewModel
    {
        private ObservableCollection<Guild> _guilds = new ObservableCollection<Guild>();
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        private GuildAddViewModel _guildAddViewModel;
        private UsersAddViewModel _usersAddViewModel;
        private UsersInGuildAddViewModel _usersInGuildAddViewModel;

        public MainUserInGuildAddViewModel(WarDbContext dbContext) : base(dbContext)
        {
            GuildAddViewModel = new GuildAddViewModel(_guilds, dbContext);
            UserAddViewModel = new UsersAddViewModel(_users, dbContext);
            UsersInGuildAddViewModel = new UsersInGuildAddViewModel(_guilds, _users, dbContext);
        }
        
        public UsersAddViewModel UserAddViewModel
        {
            get { return _usersAddViewModel; }
            set
            {
                _usersAddViewModel = value;
                RaisePropertyChanged(nameof(UserAddViewModel));
            }
        }
        public GuildAddViewModel GuildAddViewModel
        {
            get { return _guildAddViewModel; }
            set
            {
                _guildAddViewModel = value;
                RaisePropertyChanged(nameof(GuildAddViewModel));
            }
        }
        public UsersInGuildAddViewModel UsersInGuildAddViewModel
        {
            get { return _usersInGuildAddViewModel; }
            set
            {
                _usersInGuildAddViewModel = value;
                RaisePropertyChanged(nameof(UsersInGuildAddViewModel));
            }
        }

        public override VisiblityControls ViewModelType => VisiblityControls.UsersAdd;

        public override void Load()
        {
            Context.Guilds.Load();
            Context.Users.Load();
            _users.AddRange(Context.Users.Local.OrderBy(x=>x.Name).ToArray());
            _guilds.AddRange(Context.Guilds.Local.OrderBy(x => x.Name).ToArray());
            GuildAddViewModel.Load();
            UserAddViewModel.Load();
            UsersInGuildAddViewModel.Load();
        }
    }
}
