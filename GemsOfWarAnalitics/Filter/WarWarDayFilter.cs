using GemsOfWarsMainTypes.Model;
using System.Collections.Generic;
using System.Linq;

namespace GemsOfWarAnalitics
{
    public class WarWarDayFilter : WarDayFilter
    {
        private List<War> _wars;

        public WarWarDayFilter()
        {
            _wars = new List<War>();
        }

        public void AddWarToFilter(War additionWar)
        {
           _wars.Add(additionWar);
        }

        public WarWarDayFilter(IEnumerable<War> wars)
        {
            _wars = wars.ToList();
        }

        public override void CreatePredicate()
        {
            _predicate = warDay => _wars.Any(war => warDay.War.IsEquals(war));
        }
    }
}
