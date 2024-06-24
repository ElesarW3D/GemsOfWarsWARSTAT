using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using System.Collections.Generic;
using System.Linq;

namespace GemsOfWarAnalitics
{
    public abstract class MainStats<T> where T : WarStatsModelViewModel
    {
        protected List<T> _statContainsList;
        protected Filter<T> _filterToDifference;
        public MainStats(params T[] statContainsList)
        {
            _statContainsList = statContainsList.ToList();
        }

        public abstract MainStats<T> SetFilter(Filter<T> filter);

        public void SetFilterDifference(Filter<T> filter)
        {
            _filterToDifference = filter;
        }


        public CalcStat  Calculate()
        {
            var calcStat =  CalcStatisticTo(_statContainsList);
            if (_filterToDifference.IsNotNull())
            {
                var differenceStat = _statContainsList.Where(w => _filterToDifference.GetPredicate()(w));
                if (differenceStat.Any())
                {
                    calcStat.Difference = CalcStatisticTo(differenceStat);
                }
            }
            
            return calcStat;
        }

        protected abstract CalcStat CalcStatisticTo(IEnumerable<T> days);
      
    }



}
