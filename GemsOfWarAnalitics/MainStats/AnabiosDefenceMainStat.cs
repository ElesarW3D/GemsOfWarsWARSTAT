using GemsOfWarsMainTypes.Model;
using System.Collections.Generic;
using System.Linq;

namespace GemsOfWarAnalitics.MainStats
{
    public enum AnabiosDefenceStrategy
    {
        CalcEnemy,
        CalcAnabios,
        DefenceEfficiency
    }



    public class AnabiosDefenceMainStat : MainStats<RealWarState>
    {
        public AnabiosDefenceStrategy Strategy { get; set; } = AnabiosDefenceStrategy.CalcEnemy;
        public AnabiosDefenceMainStat(params RealWarState[] warDays) : base(warDays)
        {
        }

        public override MainStats<RealWarState> SetFilter(Filter<RealWarState> filter)
        {
            var adm = new AnabiosDefenceMainStat(_statContainsList.Where(x => filter.GetPredicate()(x)).ToArray());
            adm.Strategy = Strategy;
            return adm;
        }

        protected override CalcStat CalcStatisticTo(IEnumerable<RealWarState> states)
        {
            var wins = 0;
            var losers = 0;
            var count = 0;
            var efficiency = 0.0;
            foreach (var item in states)
            {
                int winsIn = 0;
                switch (Strategy)
                {
                    case AnabiosDefenceStrategy.CalcEnemy:
                        winsIn = item.CountAttack - item.CountLossEnemy;
                        wins += item.CountLossEnemy;
                        count += item.CountAttack;
                        losers += winsIn;
                        break;
                    case AnabiosDefenceStrategy.CalcAnabios:
                        winsIn = item.AnabiosAttack - item.AnabiosLoss;
                        wins += item.AnabiosLoss;
                        count += item.AnabiosAttack;
                        losers += winsIn;
                        break;
                    case AnabiosDefenceStrategy.DefenceEfficiency:
                        efficiency =  item.CountLossEnemy / (1.0 * item.CountAttack) - item.AnabiosLoss / (1.0 * item.AnabiosAttack);
                        break;
                }
              
               
            }
            if (Strategy != AnabiosDefenceStrategy.DefenceEfficiency)
            {
                var wr = wins * 1.0 / count;
                var lr = losers * 1.0 / count;
                return new CalcStat(wins, losers, wr, lr, count);
            }
            return new CalcStat(0, 0, efficiency, 0, 1);
        }
    }
}
