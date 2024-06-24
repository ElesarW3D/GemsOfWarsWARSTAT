
using GalaSoft.MvvmLight;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using static GemsOfWarsMainTypes.GlobalConstants;
using System.Linq;

namespace GemsOfWarsMainTypes.Model
{
    public class Defence : WarStatsModelViewModel, IExchange<Defence>, IDefences, IDisplayName
    {
        private Unit _unit1;
        private Unit _unit2;
        private Unit _unit3;
        private Unit _unit4;
        private string _code;
        private HeroClass _heroClass;
        public Unit Units1
        {
            get => _unit1;
            set
            {
                _unit1 = value;
                RaisePropertyChanged(nameof(Units1));
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public Unit Units2
        {
            get => _unit2;
            set
            {
                _unit2 = value;
                RaisePropertyChanged(nameof(Units2));
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public Unit Units3
        {
            get => _unit3;
            set
            {
                _unit3 = value;
                RaisePropertyChanged(nameof(Units3));
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public Unit Units4
        {
            get => _unit4;
            set
            {
                _unit4 = value;
                RaisePropertyChanged(nameof(Units4));
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

       
        public HeroClass HeroClass
        {
            get => _heroClass;
            set
            {
                if (value != _heroClass)
                {
                    _heroClass = value;
                    RaisePropertyChanged(nameof(HeroClass));
                }
            }
        }
        public IEnumerable<Unit> GetUnits()
        {
            yield return _unit1;
            yield return _unit2;
            yield return _unit3;
            yield return _unit4;
        }

        public string Code 
        { 
            get => _code;
            set
            {
                _code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }

        [NotMapped]
        public string DisplayName => DefenceDisplayName.Args(
            Units1?.DisplayName ?? "",
            Units2?.DisplayName ?? "",
            Units3?.DisplayName ?? "",
            Units4?.DisplayName ?? "");
        
        [NotMapped]
        public string TabulationDisplayName => DefenceTabulationDisplayName.Args(
           Units1?.DisplayName ?? "",
           Units2?.DisplayName ?? "",
           Units3?.DisplayName ?? "",
           Units4?.DisplayName ?? "");

        public override object Clone()
        {
            return PersoneClone<Defence>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is Defence def &&
                def.Units1.IsEquals(Units1) &&
                def.Units2.IsEquals(Units2) &&
                def.Units3.IsEquals(Units3) &&
                def.Units4.IsEquals(Units4) &&
                IsBothNullOrEqual(def.HeroClass, HeroClass) &&
                string.Compare(Code,def.Code) == 0;
        }

        public bool IsEqualsWithoutCode(object obj)
        {
            return obj is Defence def &&
                def.Units1.IsEquals(Units1) &&
                def.Units2.IsEquals(Units2) &&
                def.Units3.IsEquals(Units3) &&
                def.Units4.IsEquals(Units4) && 
                def.HeroClass.IsEquals(HeroClass);
        }

        public void ReadFromItem(Defence item)
        {
            Units1 = item.Units1;
            Units2 = item.Units2;
            Units3 = item.Units3;
            Units4 = item.Units4;
            Code = item.Code ?? null;
            HeroClass = item.HeroClass ?? null;
        }

        public void WriteToItem(Defence item)
        {
            item.Units1 = Units1.PersoneClone<Unit>();
            item.Units2 = Units2.PersoneClone<Unit>();
            item.Units3 = Units3.PersoneClone<Unit>();
            item.Units4 = Units4.PersoneClone<Unit>();
            item.HeroClass = HeroClass?.PersoneClone<HeroClass>();
            item.Code = Code;
        }

        public bool BeAnalog(Defence other)
        {
            var otherUnits = new Stack<Unit>(other.GetUnits());
            var thisUnits = GetUnits().ToList();
            while (otherUnits.Any())
            {
                var otherUnit = otherUnits.Pop();
                foreach (var unit in thisUnits)
                {
                    if (unit.BeAnalog(otherUnit))
                    {
                        thisUnits.Remove(unit);
                        break;
                    }
                }
                if (thisUnits.Count != otherUnits.Count)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
