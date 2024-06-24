using GemsOfWarsMainTypes.SubType;
using System;
using System.Collections.Generic;

namespace GemsOfWarsMainTypes.Model
{
    public class WarColorMap : WarStatsModelViewModel, IExchange<WarColorMap>
    {
        private ColorUnits _colorDay1;
        private ColorUnits _colorDay2;
        private ColorUnits _colorDay3;
        private ColorUnits _colorDay4;
        private ColorUnits _colorDay5;
        private ColorUnits _colorDay6;

        public ColorUnits ColorDay1
        {
            get => _colorDay1;
            set
            {
                if (_colorDay1 != value)
                {
                    _colorDay1 = value;
                    RaisePropertyChanged(nameof(ColorDay1));
                }
            }
        }

        public ColorUnits ColorDay2
        {
            get => _colorDay2;
            set
            {
                if (_colorDay2 != value)
                {
                    _colorDay2 = value;
                    RaisePropertyChanged(nameof(ColorDay2));
                }
            }
        }

        public ColorUnits ColorDay3
        {
            get => _colorDay3;
            set
            {
                if (_colorDay3 != value)
                {
                    _colorDay3 = value;
                    RaisePropertyChanged(nameof(ColorDay3));
                }
            }
        }

        public ColorUnits ColorDay4
        {
            get => _colorDay4;
            set
            {
                if (_colorDay4 != value)
                {
                    _colorDay4 = value;
                    RaisePropertyChanged(nameof(ColorDay4));
                }
            }
        }

        public ColorUnits ColorDay5
        {
            get => _colorDay5;
            set
            {
                if (_colorDay5 != value)
                {
                    _colorDay5 = value;
                    RaisePropertyChanged(nameof(ColorDay5));
                }
            }
        }
        public ColorUnits ColorDay6
        {
            get => _colorDay6;
            set
            {
                if (_colorDay6 != value)
                {
                    _colorDay6 = value;
                    RaisePropertyChanged(nameof(ColorDay6));
                }
            }
        }
        public WarColorMap()
        {

        }

        public WarColorMap(ColorUnits[] colorsDay)
        {
            if (colorsDay.Length != 6)
            {
                throw new ArgumentException();
            }
            ColorDay1 = colorsDay[0];
            ColorDay2 = colorsDay[1];
            ColorDay3 = colorsDay[2];
            ColorDay4 = colorsDay[3];
            ColorDay5 = colorsDay[4];
            ColorDay6 = colorsDay[5];
        }

        public override object Clone()
        {
            return PersoneClone<WarColorMap>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is WarColorMap RealWarState &&
               RealWarState.ColorDay1 == ColorDay1 &&
               RealWarState.ColorDay2 == ColorDay2 &&
               RealWarState.ColorDay3 == ColorDay3 &&
               RealWarState.ColorDay4 == ColorDay4 &&
               RealWarState.ColorDay5 == ColorDay5 &&
               RealWarState.ColorDay6 == ColorDay6;

        }

        public void ReadFromItem(WarColorMap item)
        {
            ColorDay1 = item.ColorDay1;
            ColorDay2 = item.ColorDay2;
            ColorDay3 = item.ColorDay3;
            ColorDay4 = item.ColorDay4;
            ColorDay5 = item.ColorDay5;
            ColorDay6 = item.ColorDay6;
        }

        public void WriteToItem(WarColorMap item)
        {
            item.ColorDay1 = ColorDay1;
            item.ColorDay2 = ColorDay2; 
            item.ColorDay3 = ColorDay3; 
            item.ColorDay4 = ColorDay4; 
            item.ColorDay5 = ColorDay5; 
            item.ColorDay6 = ColorDay6;
        }

        public Dictionary<int, ColorUnits> GetDayNumber()
        {
            return new Dictionary<int, ColorUnits>
            {
                { 1, ColorDay1 },
                { 2, ColorDay2 },
                { 3, ColorDay3 },
                { 4, ColorDay4 },
                { 5, ColorDay5 },
                { 6, ColorDay6 },
            };
        }
    }
}
