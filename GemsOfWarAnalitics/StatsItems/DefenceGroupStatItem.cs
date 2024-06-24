using GemsOfWarsMainTypes;
using GemsOfWarsMainTypes.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarAnalitics
{
    public class DefenceGroupStatItem : StatItem
    {
        private List<StatDefence> _statDefences;
        private StatDefence _mainItem;
        public DefenceGroupStatItem(List<StatDefence> statDefences):base(false, null)
        {
            this._statDefences = statDefences;
        }
        public void CalculateNewStatistic()
        {
            var wins = 0;
            var losers = 0;
            var count = 0;
            _mainItem = _statDefences.First();
            foreach (var item in _statDefences)
            {
                wins += item.Statictic.Victories;
              
                losers += item.Statictic.Losses;
             
                count += item.Count;
                if (item.Statictic.WinRate > _mainItem.WinRate)
                {
                    _mainItem = item;
                }
            }
           
            var wr = wins * 1.0 / count;
            var lr = losers * 1.0 / count;
            Statictic =  new CalcStat(wins, losers, wr, lr, count);
        }

        public override string DisValue => throw new NotImplementedException();

        public override string PrintValue => throw new NotImplementedException();

        public override string DisValueDiff => throw new NotImplementedException();

        public override string PrintValueDiff
        {
            get
            {
                var builder = new StringBuilder();

                builder.Append(GlobalConstants.PrintValueDiff.Args(_mainItem.Defence.TabulationDisplayName, _mainItem.Defence.HeroClass?.Name ?? string.Empty, Statictic.WinRate, Statictic.SpecificCountName, PrintDifference));
                if (_statDefences.Count > 1)
                {
                    builder.AppendLine();
                    foreach (var item in _statDefences)
                    {
                        builder.Append(" \t");
                        builder.Append(item.PrintValueDiff);
                    }
                }
                else
                {
                    builder.Append(_mainItem.AddCode());
                }
                
                return builder.ToString();
            }

        }

        public override string DisplayName => throw new NotImplementedException();
    }
}
