using GemsOfWarsMainTypes.SubType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsMainTypes.Model
{
    public class ColorsCount : WarStatsModelViewModel
    {
        private ColorUnits _color;
        private int _count;
        public ColorUnits Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    RaisePropertyChanged(nameof(Color));
                }
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                if (_count != value)
                {
                    _count = value;
                    RaisePropertyChanged(nameof(Count));
                }
            }
        }

        public override object Clone()
        {
            return new ColorsCount() { Color = this.Color, Count = this.Count };
        }

        public override bool Equals(object obj)
        {
            return obj is ColorsCount count &&
                   _color == count._color &&
                   _count == count._count;
        }

        public override int GetHashCode()
        {
            int hashCode = -58305626;
            hashCode = hashCode * -1521134295 + _color.GetHashCode();
            hashCode = hashCode * -1521134295 + _count.GetHashCode();
            return hashCode;
        }

        public override bool IsEquals(object obj)
        {
            return obj is ColorsCount colorsCount &&
                 colorsCount.Color == Color &&
                 colorsCount.Count == Count;
        }
    }
}
