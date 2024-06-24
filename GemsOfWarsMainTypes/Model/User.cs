using System;

namespace GemsOfWarsMainTypes.Model
{
    public class User : WarStatsModelViewModel, IExchange<User>
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
            return PersoneClone<User>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is User unit &&
                unit.Name == Name;
        }

        public void ReadFromItem(User item)
        {
            Name = item.Name;
            
        }

        public void WriteToItem(User item)
        {
            item.Name = Name;
        }
    }
}
