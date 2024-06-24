using GalaSoft.MvvmLight;
using GemsOfWarsMainTypes.Model.Interface;
using GemsOfWarsMainTypes.SubType;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GemsOfWarsMainTypes.Model
{
    public class WarDay : WarStatsModelViewModel, IExchange<WarDay>, IDefences, IDisplayName
    {
        private ColorUnits _colorDay;
        private User _user;
        private Defence _defence;
        private War _war;
        private int _losses;
        private int _victories;
        private MultipleFileLocations _multipleFileLocations;
        public ColorUnits ColorDay
        {
            get => _colorDay;
            set
            {
                _colorDay = value;
                RaisePropertyChanged(nameof(ColorDay));
            }
        }

        [Required]
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(nameof(User));
            }
        }

        [Required]
        public Defence Defence
        {
            get => _defence;
            set
            {
                _defence = value;
                RaisePropertyChanged(nameof(Defence));
            }
        }

        [Required]
        public War War
        {
            get => _war;
            set
            {
                _war = value;
                RaisePropertyChanged(nameof(War));
            }
        }

        public int Losses
        {
            get => _losses;
            set
            {
                _losses = value;
                RaisePropertyChanged(nameof(Losses));
            }
        }

        public int Victories
        {
            get => _victories;
            set
            {
                _victories = value;
                RaisePropertyChanged(nameof(Victories));
            }
        }

        public string DisplayName => Id.ToString();

        [NotMapped]
        public Unit Units1 { get => ((IDefences)Defence)?.Units1; set => ((IDefences)Defence).Units1 = value; }
        [NotMapped]                                    
        public Unit Units2 { get => ((IDefences)Defence)?.Units2; set => ((IDefences)Defence).Units2 = value; }
        [NotMapped]                                    
        public Unit Units3 { get => ((IDefences)Defence)?.Units3; set => ((IDefences)Defence).Units3 = value; }
        [NotMapped]                                  
        public Unit Units4 { get => ((IDefences)Defence)?.Units4; set => ((IDefences)Defence).Units4 = value; }

        public override object Clone()
        {
            return PersoneClone<WarDay>();
        }

        public void ReadFromItem(WarDay item)
        {
            ColorDay = item.ColorDay;
            Defence = item.Defence?.PersoneClone<Defence>() ?? null;
                    War = item.War?.PersoneClone<War>() ?? null;
            Losses = item.Losses;
            Victories = item.Victories;
            User = item.User?.PersoneClone<User>() ?? null;
        }

        public void WriteToItem(WarDay item)
        {
            item.ColorDay = ColorDay;
            item.Defence = Defence.PersoneClone<Defence>();
       
            item.War = War.PersoneClone<War>();
            item.Losses = Losses;
            item.Victories = Victories;
            item.User = User.PersoneClone<User>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is WarDay warDay &&
                warDay.ColorDay == ColorDay &&
                warDay.Defence.IsEquals(Defence) &&
                warDay.War.IsEquals(War) &&
                warDay.User.IsEquals(User);
        }
    }
}
