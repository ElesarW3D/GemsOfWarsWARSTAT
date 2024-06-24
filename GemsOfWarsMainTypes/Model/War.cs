using GalaSoft.MvvmLight;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static GemsOfWarsMainTypes.GlobalConstants;

namespace GemsOfWarsMainTypes.Model
{
    public class War : WarStatsModelViewModel, IExchange<War>, IDisplayName
    {
        private DateTime _dateStart;
        private WarColorMap _colorMap;

        public DateTime DateStart 
        { 
            get => _dateStart;
            set
            {
                if (_dateStart != value)
                {
                    _dateStart = value;
                    RaisePropertyChanged(nameof(DateStart));
                    RaisePropertyChanged(nameof(DisplayName));
                }
            }
        }

        public ICollection<WarDay> Days { get; set; }

        public WarColorMap MapColor
        {
            get => _colorMap;
            set
            {
                if (_colorMap != value)
                {
                    _colorMap = value;
                    RaisePropertyChanged(nameof(MapColor));
                }
            }
        }


        [NotMapped]
        public string DisplayName => DateStart.ToString("dd MMMM yyyy");

        public void ReadFromItem(War item)
        {
            DateStart = item.DateStart;
            Days = item.Days;
            MapColor = item.MapColor?.PersoneClone<WarColorMap>();
        }

        public void WriteToItem(War item)
        {
            item.DateStart = DateStart;
            item.Days = Days;
            item.MapColor = MapColor?.PersoneClone<WarColorMap>();
        }

        public override object Clone()
        {
            return PersoneClone<War>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is War war &&
                war.DateStart == DateStart;

        }


    }
}
