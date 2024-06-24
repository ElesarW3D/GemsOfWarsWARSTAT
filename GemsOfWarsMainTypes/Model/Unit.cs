using GalaSoft.MvvmLight;
using GemsOfWarsMainTypes.Model.Interface;
using GemsOfWarsMainTypes.SubType;
using System.ComponentModel.DataAnnotations.Schema;

namespace GemsOfWarsMainTypes.Model
{
    public class Unit : WarStatsModelViewModel, IExchange<Unit>, IDisplayName
    {
       
        private string _name;
        private ColorUnits _colorUnits;

        public string Name 
        { 
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        private int _gameId;

        public int GameId
        {
            get => _gameId;
            set
            {
                _gameId = value;
                RaisePropertyChanged(nameof(GameId));
            }
        }

        public ColorUnits ColorUnits 
        {
            get => _colorUnits; 
            set
            {
                if (_colorUnits != value)
                {
                    _colorUnits = value;
                    RaisePropertyChanged(nameof(ColorUnits));
                }
            }
        }


        [NotMapped]
        public virtual string DisplayName => Name ?? "";

        public override object Clone()
        {
            return PersoneClone<Unit>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is Unit unit &&
                unit.Name.Equals(Name, System.StringComparison.OrdinalIgnoreCase) &&
                unit.Name.Equals(Name) &&
                unit.GameId == GameId &&
                unit.ColorUnits == ColorUnits;
        }

        public void ReadFromItem(Unit item)
        {
            Name = item.Name;
            ColorUnits = item.ColorUnits;
            GameId = item.GameId;
        }

        public void WriteToItem(Unit item)
        {
            item.Name = Name;
            item.ColorUnits = ColorUnits;
            item.GameId = GameId;
        }

        public virtual bool BeAnalog(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }
            return unit.Id == Id;
        }
    }

    public class HeroWeapon : Unit, IExchange<HeroWeapon>
    {
        public int HeroClassId { get; set; }
        public HeroClass HeroClass { get; set; }

        [NotMapped]
        public override string DisplayName
        {
            get
            {
                if (HeroClass == null || string.IsNullOrEmpty(HeroClass.Name))
                {
                    return string.Empty;
                }
                var  name = HeroClass?.Name;
                if (name.Length > 8)
                {
                    name = name.Substring(0, 8);
                }
                return Name + " (" + name + ")";
            }
        }

        public override object Clone()
        {
            return PersoneClone<HeroWeapon>();
        }

        public void ReadFromItem(HeroWeapon item)
        {
            ReadFromItem((Unit)item);
            HeroClass = item.HeroClass.PersoneClone<HeroClass>();
        }

        public void WriteToItem(HeroWeapon item)
        {
            item.WriteToItem((Unit)this);
            item.HeroClass.WriteToItem(HeroClass);
        }

        public override bool IsEquals(object obj)
        {
            return base.IsEquals(obj) &&
                    obj is HeroWeapon weapon &&
                    weapon.HeroClass.IsEquals(HeroClass);
               
        }

        public override bool BeAnalog(Unit unit)
        {
            return IsEquals(unit);
        }
    }
}
