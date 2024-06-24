using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarAnalitics.MainStats
{
    public class WarDayMainStats : MainStats<WarDay>
    {
        public int MinCount { get; } = -1;
        public WarDayMainStats(params WarDay[] warDays):base(warDays)
        {
        }

        public WarDayMainStats(int minCount, params WarDay[] warDays) : base(warDays)
        {
            MinCount = minCount;
        }

        public override MainStats<WarDay> SetFilter(Filter<WarDay> filter)
        {
            return new WarDayMainStats(MinCount, _statContainsList.Where(x => filter.GetPredicate()(x)).ToArray());
        }

        protected override CalcStat CalcStatisticTo(IEnumerable<WarDay> days)
        {
            var wins = 0;
            var losers = 0;
            var count = 0;
            var realCount = 0;
            foreach (var item in days)
            {
                wins += item.Victories;
                realCount += item.Victories;
                losers += item.Losses;
                realCount += item.Losses;
            }
            if (MinCount > 0 && realCount < MinCount)
            {
                count = MinCount;
            }
            else
            {
                count = realCount;
            }
            var wr = wins * 1.0 / count;
            var lr = losers * 1.0 / count;
            return new CalcStat(wins, losers, wr, lr, count);
        }
       
    }
}
