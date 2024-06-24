using GemsOfWarsMainTypes.Model;
using System.Collections.Generic;
using System.Linq;

namespace GemsOfWarAnalitics.Filter
{
    public class WarRealStatFilter : RealStatFilter
    {
        private List<War> _wars;

        public WarRealStatFilter()
        {
            _wars = new List<War>();
        }

        public void AddWarToFilter(War additionWar)
        {
            _wars.Add(additionWar);
        }

        public WarRealStatFilter(IEnumerable<War> wars)
        {
            _wars = wars.ToList();
        }

        public override void CreatePredicate()
        {
            _predicate = warDay => _wars.Any(war => warDay.War.IsEquals(war));
        }
    }
}
