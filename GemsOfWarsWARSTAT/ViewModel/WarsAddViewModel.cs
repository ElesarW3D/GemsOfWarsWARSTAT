using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GemsOfWarsWARSTAT.DataContext;
using GemsOfWarsMainTypes.Model;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using GemsOfWarsMainTypes.Extension;
using System.Collections.ObjectModel;
using static GemsOfWarsMainTypes.GlobalConstants;
using GemsOfWarsWARSTAT.Services;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class WarsAddViewModel : WarStatsBaseViewModel
    {
        private WarGuildAddViewModel _warGuildAddViewModel; 
        private ObservableCollection<War> _wars;
        public ObservableCollection<War> Wars
        {
            get { return _wars; }
            set
            {
                _wars = value;
                RaisePropertyChanged(nameof(Wars));
            }
        }

        public WarGuildAddViewModel GuildAddViewModel
        {
            get { return _warGuildAddViewModel; }
            set
            {
                _warGuildAddViewModel = value;
                RaisePropertyChanged(nameof(GuildAddViewModel));
            }
        }
        
        public WarsAddViewModel(WarDbContext context):base(context)
        {
            _warGuildAddViewModel = new WarGuildAddViewModel(context);
        }

        public override void Load()
        {
            Context.Wars.Load();
            Wars = Context.Wars.Include(x=>x.MapColor).ToObservableCollection();
            SetSelectToLast(Wars);
            GuildAddViewModel.Load();
            GuildAddViewModel.SetNewWar(SelectionWar);
        }

        protected override void OnDeleteItem()
        {
            var war = Context.Wars.FirstOrDefault(it => it.Id == SelectionWar.Id);

            Wars.Remove(SelectionWar);
            Selection = null;
            if (war != null)
            {
                Context.Wars.Remove(war);
            }

            DoOnUpdateItem();

            SetSelectToLast(Wars);
        }

        protected override void OnUpdateItem()
        {
            var wars = Context.Wars.Local.ToArray();
            if (CheckValidation(wars, DisplayWar))
            {
                return;
            }

            var war = Context.Wars.FirstOrDefault(it => it.Id == SelectionWar.Id);
            if (war != null)
            {
                war.ReadFromItem(SelectionWar);
            }
            else
            {
               war = Context.Wars.Add(SelectionWar);
            }

            DoOnUpdateItem();
        }

        protected override void OnAddItem()
        {
            var newItem = new War()
            {
                DateStart = DateTime.Now,
            };

            Selection = newItem;
            Wars.Add(newItem);
            DoOnAddItem();
        }

        public War SelectionWar => Selection as War;

        public override WarStatsModelViewModel Selection
        {
            get { return _selection; }
            set
            {
                base.Selection = value;
                _warGuildAddViewModel?.SetNewWar(SelectionWar);
            }
        }

        public override VisiblityControls ViewModelType => VisiblityControls.WarsAdd;
    }
}
