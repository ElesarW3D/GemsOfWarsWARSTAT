using GemsOfWarsMainTypes.Model.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace GemsOfWarsMainTypes.Model
{
    public class WarGuild : WarStatsModelViewModel, IExchange<WarGuild>, IDisplayName
    {
        private Guild _guild;
        private War _war;
        public int _basket;

        public Guild Guild
        {
            get { return _guild; }
            set
            {
                if (_guild != value)
                {
                    _guild = value;
                    RaisePropertyChanged(nameof(Guild));
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
                }
            }
        }

        public int Basket
        {
            get => _basket;
            set
            {
                _basket = value;
                RaisePropertyChanged(nameof(Basket));
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        [NotMapped]
        public string DisplayName => War.DisplayName + " " + Guild.Name + $" Basket {Basket}";

        public override object Clone()
        {
            return PersoneClone<WarGuild>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is WarGuild warGuild &&
               warGuild.Guild.Id == Guild.Id &&
               warGuild.War.Id == War.Id;
        }

        public void ReadFromItem(WarGuild item)
        {
            Guild = item.Guild;
            Basket = item.Basket;
            War = item.War;
        }

        public void WriteToItem(WarGuild item)
        {
            item.Guild = Guild;
            item.Basket = Basket;
            item.War = War;
        }
    }
}
