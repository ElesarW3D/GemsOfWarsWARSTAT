using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsMainTypes.Model
{
    public class Guild : WarStatsModelViewModel, IExchange<Guild>
    {
        private string _name;

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
            return PersoneClone<Guild>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is Guild unit &&
                unit.Name == Name;
        }

        public void ReadFromItem(Guild item)
        {
            Name = item.Name;
        }

        public void WriteToItem(Guild item)
        {
            item.Name = Name;
        }
    }
}
