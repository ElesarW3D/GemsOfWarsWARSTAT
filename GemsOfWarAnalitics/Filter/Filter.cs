using GemsOfWarsMainTypes.Model;
using System;

namespace GemsOfWarAnalitics
{
    public abstract class Filter<T> where T : WarStatsModelViewModel
    {
        protected Predicate<T> _predicate;
        public abstract void CreatePredicate();

        public Predicate<T> GetPredicate() => _predicate;
    }

    public abstract class WarDayFilter : Filter<WarDay>
    {

    }

    public abstract class RealStatFilter : Filter<RealWarState>
    {

    }
}
