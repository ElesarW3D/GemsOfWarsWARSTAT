using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using GemsOfWarsWARSTAT.DataContext;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using GemsOfWarsMainTypes.SubType;
using static GemsOfWarsMainTypes.GlobalConstants;
using GalaSoft.MvvmLight.CommandWpf;
using GemsOfWarsMainTypes;
using System.Windows;
using GemsOfWarsWARSTAT.Services;

namespace GemsOfWarsWARSTAT.ViewModel
{
    public class RealStatAddViewModel : WarStatsBaseViewModel
    {
        private ObservableCollection<War> _warList;
        private ObservableCollection<RealWarState> _realStats;
        private ObservableCollection<Guild> _guilds;
        private string _textSearch;

        public string TextSearch
        {
            get => _textSearch;
            set
            {
                if (value != _textSearch)
                {
                    _textSearch = value;
                    RaisePropertyChanged(nameof(TextSearch));
                }
            }
        }

        public ObservableCollection<War> Wars
        {
            get { return _warList; }
            set
            {
                _warList = value;
                RaisePropertyChanged(nameof(Wars));
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

        public ObservableCollection<RealWarState> RealStats
        {
            get { return _realStats; }
            set
            {
                _realStats = value;
                RaisePropertyChanged(nameof(RealStats));
            }
        }


        public RealStatAddViewModel(WarDbContext context) : base(context)
        {
            AddGroupLostFocus = new RelayCommand(OnLostFocusGroup);
        }

        private void OnLostFocusGroup()
        {
            if (string.IsNullOrEmpty(TextSearch))
            {
                return;
            }
            var guild = Context.Guilds.FirstOrDefault(x => x.Name == TextSearch);
            if (guild != null)
            {
                if (SelectionRealStat.EnemyGuild != guild)
                {
                    SelectionRealStat.EnemyGuild = guild;
                }
                return;
            }
            var message = GlobalConstants.Messages.AddGuild.Args(TextSearch);
            var result = MessageBox.Show(message, "Добавить гильдию", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var addGuild = new Guild();
                addGuild.Name = TextSearch;
                Context.Guilds.Add(addGuild);
                Context.SaveChanges();
                SelectionRealStat.EnemyGuild = addGuild;
                Guilds.Add(addGuild);
            }
            
        }

        public override WarStatsModelViewModel Selection
        {
            get { return _selection; }
            set
            {
                base.Selection = value;
               
                RaisePropertyChanged(nameof(Selection));
                RaisePropertyChanged(nameof(SelectionRealStat));
            }
        }

        public override void Load()
        {
            Context.Wars.Load();
            Wars = Context.Wars.Local.ToObservableCollection();

            Context.Guilds.Load();
            Guilds = Context.Guilds.Local.ToObservableCollection();

            Context.RealWarStates.Load();

            RealStats = Context.RealWarStates.Local
                .OrderByDescending(x => x.War.DateStart)
                .ThenBy(x => x.ColorDay)
                .ToObservableCollection();

            SetSelectToLast(RealStats);
        }


        protected override void OnAddItem()
        {
            var newItem = new RealWarState()
            {
                War = Wars.LastOrDefault(),
                ColorDay = ColorUnits.Red,
                CountAttack = 150,
                CountLossEnemy = -1,
                AnabiosAttack = 150,
                AnabiosLoss = -1,
            };

            Selection = newItem;
            RealStats.Add(newItem);
            DoOnAddItem();
        }

        protected override void OnDeleteItem()
        {
            var realStats = Context.RealWarStates.FirstOrDefault(it => it.Id == Selection.Id);

            RealStats.Remove(SelectionRealStat);
            Selection = null;
            if (realStats != null)
            {
                Context.RealWarStates.Remove(realStats);
            }

            DoOnUpdateItem();

            SetSelectToLast(RealStats);
        }
        public RelayCommand AddGroupLostFocus { get ; set ; }
        public RealWarState SelectionRealStat => Selection as RealWarState;

        public override VisiblityControls ViewModelType => VisiblityControls.RealStat;

        protected override void OnUpdateItem()
        {
            var stats = Context.RealWarStates.Local.ToArray();
            if (CheckValidation(stats, DisplayRealStat))
            {
                return;
            }

            var realWarState = Context.RealWarStates.FirstOrDefault(it => it.Id == SelectionRealStat.Id);
            if (realWarState != null)
            {
                realWarState.ReadFromItem(SelectionRealStat);
            }
            else
            {
                Context.RealWarStates.Add(SelectionRealStat);
            }

            DoOnUpdateItem();
        }
    }
}
