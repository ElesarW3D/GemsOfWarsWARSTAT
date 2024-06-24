using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsMainTypes.Model
{
    public class MultipleFileLocations : WarStatsModelViewModel, IExchange<MultipleFileLocations>
    {
        private FileLocation _fileLocation;
        private WarDay _warDay;
        public FileLocation FileLocation
        {
            get => _fileLocation;
            set
            {
                _fileLocation = value;
                RaisePropertyChanged(nameof(FileLocation));
            }
        }

        public WarDay WarDay
        {
            get => _warDay;
            set
            {
                _warDay = value;
                RaisePropertyChanged(nameof(WarDay));
            }
        }

        public override object Clone()
        {
            return PersoneClone<MultipleFileLocations>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is MultipleFileLocations multipleFileLocations &&
                multipleFileLocations.FileLocation.IsEquals(FileLocation);
        }

        public void ReadFromItem(MultipleFileLocations item)
        {
            FileLocation = item.FileLocation;
            WarDay = item.WarDay;
        }

        public void WriteToItem(MultipleFileLocations item)
        {
            item.FileLocation = FileLocation;
            item.WarDay = WarDay;
        }
    }
}
