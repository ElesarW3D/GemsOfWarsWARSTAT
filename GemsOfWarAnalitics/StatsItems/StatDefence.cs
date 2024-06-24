using GemsOfWarsMainTypes;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;

namespace GemsOfWarAnalitics
{
    public class StatDefence : StatItem
    {
        public StatDefence(CalcStat statictic, Defence defence, bool useDiff) : base(useDiff, statictic)
        {
            Defence = defence;
        }

        public Defence Defence { get; }

        public override string DisValue => GlobalConstants.DisValue.Args(Defence.DisplayName, Statictic.WinRate, Statictic.SpecificCountName) + AddCode();

        public override string PrintValue => GlobalConstants.PrintValue.Args(Defence.DisplayName, Statictic.WinRate, Statictic.SpecificCountName) + AddCode();

        public override string DisValueDiff => GlobalConstants.DisValueDiff.Args(Defence.DisplayName, Statictic.WinRate, Statictic.SpecificCountName, PrintDifference) + AddCode();

        public override string PrintValueDiff => GlobalConstants.PrintValueDiff.Args(Defence.TabulationDisplayName, Defence.HeroClass?.Name ?? "", Statictic.WinRate, Statictic.SpecificCountName, PrintDifference) + AddCode();

        public override string DisplayName => Defence.DisplayName;

        public string AddCode() => "\t" + Defence.Code+"\n";
    }
}
