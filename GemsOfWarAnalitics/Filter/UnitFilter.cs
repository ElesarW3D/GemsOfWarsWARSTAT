using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GemsOfWarAnalitics
{
    public class UnitFilter : WarDayFilter
    {
        private Unit _unit;

        public Unit Unit { get => _unit; set => _unit = value; }

        public override void CreatePredicate()
        {
            if (Unit == null)
            {
                _predicate = (x) => false;
            }
            else
            {
                _predicate = w => w.Defence.Units1.IsEquals(_unit)
                        || w.Defence.Units2.IsEquals(_unit)
                        || w.Defence.Units3.IsEquals(_unit)
                        || w.Defence.Units4.IsEquals(_unit);
            }
        }
    }

    public class DefenceFilter : WarDayFilter
    {
        private Defence _defence;

        public Defence Defence { get => _defence; set => _defence = value; }

        public override void CreatePredicate()
        {
            if (Defence == null)
            {
                _predicate = (x) => false;
            }
            else
            {
                _predicate = w => w.Defence.IsEquals(_defence);
            }
        }
    }

    public class UserFilter : WarDayFilter
    {
        private User _user;

        public User User { get => _user; set => _user = value; }

        public override void CreatePredicate()
        {
            if (User == null)
            {
                _predicate = (x) => false;
            }
            else
            {
                _predicate = w => w.User.IsEquals(_user);
            }
        }
    }

    public class BasketsFilter : WarDayFilter
    {
        private List<WarGuild> _actualWarGuild;
        private List<UserInGuild> _actualUserInGuild;
        public BasketsFilter(List<WarGuild> warGuilds, List<UserInGuild> userInGuilds)
        {
            WarGuilds = warGuilds;
            UserInGuilds = userInGuilds;
        }

        public void SetBaskets(params int []baskets)
        {
            _actualWarGuild = WarGuilds.Where(x=>baskets.Contains(x.Basket)).ToList();
        }

        public List<WarGuild> WarGuilds { get; private set; }
        public List<UserInGuild> UserInGuilds { get; private set; }

        public override void CreatePredicate()
        {
            if (_actualWarGuild.Any())
            {
                _predicate = w => CheckWar(w);
            }
            else
            {
                _predicate = (x) => false;
            }
        }

        private bool CheckWar(WarDay w)
        {
            try
            {
                var user = w.User;
                var war = w.War;
                var warsGuild = _actualWarGuild.Where(x=>x.War.Id == war.Id).ToList();
                if (!warsGuild.Any())
                {
                    return false;
                }
                var actualUsers = UserInGuilds.Where(x => x.User.Id == user.Id);
                var guild = actualUsers.Where(x => x.InWar(war)).First().Guild;
                var isContains = warsGuild.Where(x => x.Guild.Id == guild.Id).Any();
                return isContains;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
