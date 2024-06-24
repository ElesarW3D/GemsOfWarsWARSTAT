using GemsOfWarsMainTypes;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarAnalitics
{
    public class StatUnit : StatItem
    {
        public StatUnit(CalcStat statictic, Unit unit, bool useDiff= false):base(useDiff, statictic)
        {
            Unit = unit;
        }

        public Unit Unit { get; }

        public override string DisValue => GlobalConstants.DisValue.Args(Unit.DisplayName, Statictic.WinRate, Statictic.SpecificCountName);

        public override string PrintValue => GlobalConstants.PrintValue.Args(Unit.DisplayName, Statictic.WinRate, Statictic.SpecificCountName);

        public override string DisValueDiff => GlobalConstants.DisValueDiff.Args(Unit.DisplayName, Statictic.WinRate, Statictic.SpecificCountName, PrintDifference);

        public override string PrintValueDiff => GlobalConstants.PrintValueDiff.Args(Unit.DisplayName, Statictic.WinRate, Statictic.SpecificCountName, PrintDifference);

        public override string DisplayName => Unit.DisplayName;
    }
}
