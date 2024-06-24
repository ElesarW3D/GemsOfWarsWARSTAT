using GalaSoft.MvvmLight;
using GemsOfWarsMainTypes.Extension;
using System;

namespace GemsOfWarsMainTypes.Model
{
    public abstract class WarStatsModelViewModel : ViewModelBase, ICloneable, IIdenty
    {
        private int _id;

        public abstract object Clone();
        
        public virtual T PersoneClone<T>() where T : IExchange<T>, new()
        {
            if (this is IExchange<T> writeItem)
            {
                var clone = new T();
                clone.ReadFromItem((T)writeItem);
                return clone;
            }
            
            return (T)MemberwiseClone();
        }

        public abstract bool IsEquals(object obj);

        public bool IsBothNullOrEqual(object val, object val2)
        {
            if (val.IsNull() && val2.IsNull())
            {
                return true;
            }
            if ((val.IsNotNull() && val2.IsNull()) || (val2.IsNotNull() && val.IsNull()))
            {
                return false;
            }
            return val.Equals(val2);
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
    }
}
