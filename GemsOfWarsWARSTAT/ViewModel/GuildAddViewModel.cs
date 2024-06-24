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
using static GemsOfWarsMainTypes.GlobalConstants;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class GuildAddViewModel : WarStatsBaseViewModel
    {
        private ObservableCollection<Guild> _guilds;
        public GuildAddViewModel(ObservableCollection<Guild> guilds, WarDbContext context) : base(context)
        {
            _guilds = guilds;
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

        public override void Load()
        {
            SetSelectToLast(Guilds);
        }

        protected override void OnDeleteItem()
        {
            var guild = Context.Guilds.FirstOrDefault(it => it.Id == SelectionGuild.Id);

            Guilds.Remove(SelectionGuild);
            Selection = null;
            if (guild != null)
            {
                Context.Guilds.Remove(guild);
            }

            DoOnUpdateItem();

            SetSelectToLast(Guilds);
            RaiseOnChangeItem();
        }

        protected override void OnUpdateItem()
        {
            var guilds = Context.Guilds.Local.ToArray();
            if (CheckValidation(guilds, DisplayGuild))
            {
                return;
            }

            var guild = Context.Guilds.FirstOrDefault(it => it.Id == SelectionGuild.Id);
            if (guild != null)
            {
                guild.ReadFromItem(SelectionGuild);
            }
            else
            {
                guild = Context.Guilds.Add(SelectionGuild);
            }

            DoOnUpdateItem();
            RaiseOnChangeItem();
        }

        protected override void OnAddItem()
        {
            var newItem = new Guild()
            {
                Name = NonameUnit
            };

            Selection = newItem;
            Guilds.Add(newItem);
            DoOnAddItem();
            RaiseOnChangeItem();
        }

        private void RaiseOnChangeItem()
        {
            OnChangeItem?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler OnChangeItem;

        public Guild SelectionGuild => Selection as Guild;

        public override VisiblityControls ViewModelType => VisiblityControls.WarsAdd;

       
    }
}

