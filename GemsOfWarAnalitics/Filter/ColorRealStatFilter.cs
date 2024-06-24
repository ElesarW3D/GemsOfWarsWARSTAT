using GemsOfWarsMainTypes.SubType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarAnalitics.Filter
{
    public class ColorRealStatFilter : RealStatFilter
    {
        
            private ColorUnits _colors = ColorUnits.None;

            public ColorRealStatFilter()
            {
            }

            public void AddColorToFilter(ColorUnits color)
            {
                _colors &= color;
            }

            public ColorRealStatFilter(ColorUnits colors)
            {
                _colors = colors;
            }

            public override void CreatePredicate()
            {
                _predicate = warDay => (_colors & warDay.ColorDay) == warDay.ColorDay;
            }
    }
}
