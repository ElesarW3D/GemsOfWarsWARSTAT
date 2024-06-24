using GemsOfWarsMainTypes;
using GemsOfWarsMainTypes.Extension;

namespace GemsOfWarAnalitics
{
    public abstract class StatItem : BaseStatItem
    {
        protected StatItem(bool useDifference, CalcStat statictic)
        {
            UseDifference = useDifference;
            Statictic = statictic;
        }

        protected bool UseDifference { get; set; }
        public CalcStat Statictic { get; protected set; }
        public abstract string DisValue { get; }

        public abstract string PrintValue { get; }

        public abstract string DisValueDiff { get; }

        public abstract string PrintValueDiff { get; }

        protected string PrintDifference => MPrintDifference(Statictic);

        public override string WinRatePrint => string.Format("{0:P2}", WinRate);
        public override double WinRate => Statictic?.WinRate ?? double.NaN;
        public override int Count => Statictic?.Count ?? -1;

        private string MPrintDifference(CalcStat statItem)
        {
            if (!UseDifference)
            {
                return string.Empty;
            }
           
            if (statItem.Difference == null || statItem.Difference.Count == 0)
            {
                return " Не совпали ";
            }
            var difference = statItem.WinRate - statItem.Difference.WinRate;
            
            return difference.ToString();
            //if (difference > 0)
            //{
            //    return string.Format("{0:P2}", difference);
            //}
            //return string.Format("-{0:P2}", Math.Abs(difference));
        }

        protected string PrintStatItem(CalcStat statItem)
        {
            if (statItem.Count == 0)
            {
                return " не найден \t\t";
            }
            
            return GlobalConstants.PrintStatItem.Args(statItem.WinRate, statItem.SpecificCountName);
        }

        protected string PrintStatItemWithDiff(CalcStat statItem)
        {
            var mainStat = PrintStatItem(statItem);
            var printDiff = MPrintDifference(statItem);
            return mainStat + printDiff;

        }
    }
}
