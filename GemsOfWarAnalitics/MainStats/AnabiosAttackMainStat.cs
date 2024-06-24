using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarAnalitics.MainStats
{
    public class AnabiosAttackMainStat : MainStats<RealWarState>
    {
        public AnabiosAttackMainStat(params RealWarState[] warDays) : base(warDays)
        {
        }

        public override MainStats<RealWarState> SetFilter(Filter<RealWarState> filter)
        {
            return new AnabiosAttackMainStat(_statContainsList.Where(x => filter.GetPredicate()(x)).ToArray());
        }

        protected override CalcStat CalcStatisticTo(IEnumerable<RealWarState> states)
        {
            var wins = 0;
            var losers = 0;
            var count = 0;
            foreach (var item in states)
            {
                var winsIn = item.AnabiosAttack - item.AnabiosLoss;
                wins += winsIn;
                count += item.AnabiosAttack;
                losers += item.AnabiosLoss;
            }

            var wr = wins * 1.0 / count;
            var lr = losers * 1.0 / count;

           return new CalcStat(wins, losers, wr, lr, count);
        }
    }
}
