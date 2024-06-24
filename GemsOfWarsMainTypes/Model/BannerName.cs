using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsMainTypes.Model
{
    public class BannerName : WarStatsModelViewModel
    {
        private int _gameId;
        private string _name;
            
        public int GameId
        {
            get => _gameId;
            set
            {
                _gameId = value;
                RaisePropertyChanged(nameof(GameId));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public override object Clone()
        {
            return new BannerName() { Name = this.Name, GameId = this.GameId  };
        }

        public override bool IsEquals(object obj)
        {
            return obj is BannerName banner &&
                 banner.Name == Name &&
                 banner.GameId == GameId;
        }
    }
}
