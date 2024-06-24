using GemsOfWarsMainTypes;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using System.Linq;

namespace GemsOfWarAnalitics
{
    interface IBasket
    {
        double WinRate2 { get; }
        int Count2 { get; }
        double WinRate3 { get; }
        int Count3 { get; }
        double WinRate4 { get; }
        int Count4 { get; }
    }
    static class BasketHelper
    {
        public static double GetWR(this CalcStat[] calcStats, int index)
        {
            return calcStats.Count() > index ? calcStats[index].WinRate : 0;
        }

        public static int GetCount(this CalcStat[] calcStats, int index)
        {
            return calcStats.Count() > index ? calcStats[index].Count : 0;
        }
    }

    public class StatUnitBasket : StatUnit, IBasket
    {
        private CalcStat[] _statictic;
        public StatUnitBasket(Unit unit, bool useDiff = false, params CalcStat[] statictic) : base(statictic.FirstOrDefault(), unit, useDiff)
        {
            _statictic = statictic;
        }

        public int[] Baskets { get; set; }

        public override string DisValue
        {
            get
            {
                var disValue = Unit.DisplayName + "\t";
                var index = 1;
                foreach (var basket in Baskets)
                {
                    var statistic = _statictic[index];
                    index++;
                    disValue += PrintStatItem(statistic) + "\t";
                }
                disValue += GlobalConstants.FullDisValueBasket.Args(Statictic.WinRate, Statictic.Count);
                return disValue;
            }
        }

        public override string DisValueDiff
        {
            get
            {
                var disValue = Unit.DisplayName + "\t";
                var index = 1;
                foreach (var basket in Baskets)
                {
                    var statistic = _statictic[index];
                    index++;
                    disValue += GlobalConstants.DisValueBasket.Args(basket, statistic.WinRate, statistic.Count) + "\t";
                }
                disValue += GlobalConstants.FullDisValueBasketDiff.Args(Statictic.WinRate, Statictic.Count, PrintDifference);
                return disValue;
            }
        }

        public override string PrintValueDiff
        {
            get
            {
                var disValue = Unit.DisplayName + "\t";
                var index = 1;
                foreach (var basket in Baskets)
                {
                    var statistic = _statictic[index];
                    index++;
                    disValue += PrintStatItem(statistic);
                }
                disValue += PrintStatItemWithDiff(Statictic);
                return disValue;
            }
        }


        public double WinRate2 => _statictic.GetWR(1);

        public int Count2 => _statictic.GetCount(1);

        public double WinRate3 => _statictic.GetWR(2);

        public int Count3 => _statictic.GetCount(2);

        public double WinRate4 => _statictic.GetWR(3);

        public int Count4 => _statictic.GetCount(3);

    }

    public class StatDefenceBasket : StatDefence, IBasket
    {
        private CalcStat[] _statictic;
        public StatDefenceBasket(Defence defence, bool useDiff = false, params CalcStat[] statictic) : base(statictic.FirstOrDefault(), defence, useDiff)
        {
            _statictic = statictic;
        }

        public int BasketGroupCounts { get; set; }

        public override string DisValue
        {
            get
            {
                var disValue = Defence.DisplayName + "\t";
              
                for (int i = 0; i < BasketGroupCounts; i++)
                {
                    var statistic = _statictic[i+1];
                    disValue += PrintStatItem(statistic) + "\t";
                }
                
                disValue += GlobalConstants.FullDisValueBasket.Args(Statictic.WinRate, Statictic.SpecificCountName);
                return disValue;
            }
        }

        public override string DisValueDiff
        {
            get
            {
                var disValue = Defence.DisplayName + "\t";
               
                for (int i = 0; i < BasketGroupCounts; i++)
                {
                    var statistic = _statictic[i+1];
                    disValue += GlobalConstants.DisValueBasket.Args(i, statistic.WinRate, statistic.SpecificCountName) + "\t";
                }
                
                disValue += GlobalConstants.FullDisValueBasketDiff.Args(Statictic.WinRate, Statictic.SpecificCountName, PrintDifference)+"\n";
                return disValue;
            }
        }

        public override string PrintValueDiff
        {
            get
            {
                var disValue = Defence.DisplayName + "\t";

                for (int i = 0; i < BasketGroupCounts; i++)
                {
                    var statistic = _statictic[i + 1];
                    disValue += PrintStatItem(statistic);
                    
                }
               
                disValue += PrintStatItemWithDiff(Statictic) + "\n";
                return disValue;
            }
        }

        public double WinRate2 => _statictic.GetWR(1);

        public int Count2 => _statictic.GetCount(1);

        public double WinRate3 => _statictic.GetWR(2);

        public int Count3 => _statictic.GetCount(2);

        public double WinRate4 => _statictic.GetWR(3);

        public int Count4 => _statictic.GetCount(3);

    }
}
