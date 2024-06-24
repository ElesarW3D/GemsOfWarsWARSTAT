using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace GemsOfWarsMainTypes.Model
{
    public class UserInGuild : WarStatsModelViewModel, IExchange<UserInGuild>, IDisplayName
    {
        private DateTime _dateStart;
        private DateTime? _dateFinish;
        private Guild _guild;
        private User _user;
        public DateTime DateStart
        {
            get => _dateStart;
            set
            {
                if (_dateStart != value)
                {
                    _dateStart = value;
                    RaisePropertyChanged(nameof(DateStart));
                }
            }
        }

        public DateTime? DateFinish
        {
            get => _dateFinish;
            set
            {
                if (_dateFinish != value)
                {
                    _dateFinish = value;
                    RaisePropertyChanged(nameof(DateFinish));
                }
            }
        }

        public Guild Guild
        {
            get { return _guild; }
            set
            {
                if (_guild != value)
                {
                    _guild = value;
                    RaisePropertyChanged(nameof(Guild));
                }
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


        public string DisplayName => Id.ToString();

        
        public override object Clone()
        {
            return PersoneClone<UserInGuild>();
        }

        public void ReadFromItem(UserInGuild item)
        {
            Guild = item.Guild;
            User = item.User;
            DateStart = item.DateStart;
            DateFinish = item.DateFinish;
        }

        public void WriteToItem(UserInGuild item)
        {
            item.Guild = Guild;
            item.User = User;
            item.DateStart = DateStart;
            item.DateFinish = DateFinish;
        }

        public override bool IsEquals(object obj)
        {
            return obj is UserInGuild userInGuild &&
                userInGuild.Guild.Name == Guild.Name &&
                userInGuild.User.Name == User.Name &&
                userInGuild.DateStart == DateStart &&
                userInGuild.DateFinish == DateFinish;
        }

        public bool InWar(War war)
        {
            var warDateStart = war.DateStart;
            var leftRight = warDateStart >= DateStart;
            if (!leftRight)
            {
                return false;
            }
            if (DateFinish.IsNotNull())
            {
                return DateFinish >= warDateStart;
            }
            return true;
        }
    }
}
