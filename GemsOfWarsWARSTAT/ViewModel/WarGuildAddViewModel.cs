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
    public class WarGuildAddViewModel : WarStatsBaseViewModel
    {
        private ObservableCollection<WarGuild> _warGuilds;
        private ObservableCollection<Guild> _guilds;
        private War _selectWar;
        public override VisiblityControls ViewModelType => VisiblityControls.WarsAdd;

        public ObservableCollection<WarGuild> WarGuilds
        {
            get { return _warGuilds; }
            set
            {
                _warGuilds = value;
                RaisePropertyChanged(nameof(WarGuilds));
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


        public WarGuildAddViewModel(WarDbContext context) : base(context)
        {
            WarGuilds = new ObservableCollection<WarGuild>();
        }

        public override void Load()
        {
            Context.Guilds.Load();
            Guilds = Context.Guilds.Local.ToObservableCollection();
        }

        public void SetNewWar(War selectionWar)
        {
            _selectWar = selectionWar;

            WarGuilds.Clear();
            if (selectionWar.IsNull())
            {
                return;
            }
            WarGuilds.AddRange(Context.WarsGuilds.Where(x=>x.War.Id == selectionWar.Id).ToArray());
        }

        protected override void OnAddItem()
        {
            var newItem = new WarGuild()
            {
                Guild = Guilds.First(),
                War = _selectWar,
                Basket = 0
            };

            Selection = newItem;
            WarGuilds.Add(newItem);
            DoOnAddItem();
        }

        protected override void OnDeleteItem()
        {
            var warGuild = Context.WarsGuilds.FirstOrDefault(it => it.Id == SelectionWarGuild.Id);

            WarGuilds.Remove(SelectionWarGuild);
            Selection = null;
            if (warGuild != null)
            {
                Context.WarsGuilds.Remove(warGuild);
            }

            DoOnUpdateItem();

            SetSelectToLast(WarGuilds);
        }

        protected override void OnUpdateItem()
        {
            var guildWars = Context.Wars.Local.ToArray();
            if (CheckValidation(guildWars, DisplayWarGuild))
            {
                return;
            }

            var warGuilds = Context.WarsGuilds.FirstOrDefault(it => it.Id == SelectionWarGuild.Id);
            if (warGuilds != null)
            {
                warGuilds.ReadFromItem(SelectionWarGuild);
            }
            else
            {
                warGuilds = Context.WarsGuilds.Add(SelectionWarGuild);
            }

            DoOnUpdateItem();
        }

        public WarGuild SelectionWarGuild => Selection as WarGuild;

    }
}
