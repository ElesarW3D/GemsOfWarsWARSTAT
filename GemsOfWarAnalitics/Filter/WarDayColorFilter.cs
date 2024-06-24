using GemsOfWarsMainTypes.SubType;
using GemsOfWarsMainTypes.Extension;

namespace GemsOfWarAnalitics
{
    public abstract class ColorFilter : WarDayFilter
    {
        protected ColorUnits _color;
        public ColorFilter(ColorUnits colorDefence)
        {
            _color = colorDefence;
        }


    }

    public class WarDayColorFilter : ColorFilter
    {
        public WarDayColorFilter(ColorUnits color) : base(color)
        {
        }

        public override void CreatePredicate()
        {
            if (!_color.IsUsesColor())
            {
                _predicate = (WarDay) => true;
            }
            else
            {
                _predicate = warDay => warDay.ColorDay.IsContains(_color);
            }
        }
    }

}
