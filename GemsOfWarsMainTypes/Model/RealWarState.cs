using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model.Interface;
using GemsOfWarsMainTypes.SubType;
using System.ComponentModel.DataAnnotations.Schema;

namespace GemsOfWarsMainTypes.Model
{
    public class RealWarState : WarStatsModelViewModel, IExchange<RealWarState>,IDisplayName
    {
        private War _war;
        private ColorUnits _ColorDay;
        private int _countAttack;
        private int _countLossEnemy;
        private int _anabiosAttack;
        private int _anabiosLoss;
        private Guild _enemyGuild;

        public ColorUnits ColorDay
        {
            get { return _ColorDay; }
            set
            {
                if (_ColorDay != value)
                {
                    _ColorDay = value;
                    RaisePropertyChanged(nameof(ColorDay));
                    RaisePropertyChanged(nameof(DisplayName));
                }
            }
        }

        public War War
        {
            get { return _war; }
            set
            {
                if (_war != value)
                {
                    _war = value;
                    RaisePropertyChanged(nameof(War));
                    RaisePropertyChanged(nameof(DisplayName));
                }
            }
        }

        public Guild EnemyGuild
        {
            get { return _enemyGuild; }
            set
            {
                if (_enemyGuild != value)
                {
                    _enemyGuild = value;
                    RaisePropertyChanged(nameof(EnemyGuild));
                }
            }
        }

        public int CountAttack 
        { 
            get => _countAttack;
            set
            {
                if (_countAttack != value)
                {
                    _countAttack = value;
                    RaisePropertyChanged(nameof(CountAttack));
                    RaisePropertyChanged(nameof(DisplayName));
                }
            }
        }

        public int AnabiosAttack
        {
            get => _anabiosAttack;
            set
            {
                if (_anabiosAttack != value)
                {
                    _anabiosAttack = value;
                    RaisePropertyChanged(nameof(AnabiosAttack));
                }
            }
        }

        public int AnabiosLoss
        {
            get => _anabiosLoss;
            set
            {
                if (_anabiosLoss != value)
                {
                    _anabiosLoss = value;
                    RaisePropertyChanged(nameof(AnabiosLoss));
                }
            }
        }

        public int CountLossEnemy
        {
            get => _countLossEnemy;
            set
            {
                if (_countLossEnemy != value)
                {
                    _countLossEnemy = value;
                    RaisePropertyChanged(nameof(CountLossEnemy));
                    RaisePropertyChanged(nameof(DisplayName));
                }
            }
        }

        public override object Clone()
        {
            return PersoneClone<RealWarState>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is RealWarState RealWarState &&
                RealWarState.War == War &&
                RealWarState.ColorDay == ColorDay &&
                RealWarState.CountAttack == CountAttack &&
                RealWarState.CountLossEnemy == CountLossEnemy &&
                RealWarState.AnabiosAttack == AnabiosAttack &&
                RealWarState.AnabiosLoss == AnabiosLoss &&
                RealWarState.EnemyGuild.IsEquals(EnemyGuild);
        }

        public void ReadFromItem(RealWarState item)
        {
            Id = item.Id;
            War = item.War;
            ColorDay = item.ColorDay;
            CountAttack = item.CountAttack;
            CountLossEnemy = item.CountLossEnemy;
            EnemyGuild = item.EnemyGuild?.PersoneClone<Guild>() ?? null;
            AnabiosLoss = item.AnabiosLoss;
            AnabiosAttack = item.AnabiosAttack;
        }

        public void WriteToItem(RealWarState item)
        {
            item.Id = Id;
            item.War = War;
            item.ColorDay = ColorDay;
            item.CountAttack = CountAttack;
            item.CountLossEnemy = CountLossEnemy;
            item.EnemyGuild = EnemyGuild;
            item.AnabiosLoss = AnabiosLoss;
            item.AnabiosAttack = AnabiosAttack;
        }

        [NotMapped]
        public string DisplayName => GlobalConstants.RealStateDisName.Args(War.DisplayName, ColorDay.ToString(), CountLossEnemy, CountAttack);
    }
}
