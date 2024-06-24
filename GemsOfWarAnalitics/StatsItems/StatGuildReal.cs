using GemsOfWarsMainTypes;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;

namespace GemsOfWarAnalitics
{
    public class StatGuildReal : StatItem
    {
        public StatGuildReal(CalcStat statictic, Guild guild, bool useDiff = false) : base(useDiff, statictic)
        {
            Guild = guild;
        }

        public Guild Guild { get; }

        public override string DisValue => GlobalConstants.DisValue.Args(Guild.Name, Statictic.WinRate, Statictic.Count);

        public override string PrintValue => GlobalConstants.PrintValue.Args(Guild.Name, Statictic.WinRate, Statictic.Count);

        public override string DisValueDiff => GlobalConstants.DisValueDiff.Args(Guild.Name, Statictic.WinRate, Statictic.Count, PrintDifference);

        public override string PrintValueDiff => GlobalConstants.PrintValueDiff.Args(Guild.Name, Statictic.WinRate, Statictic.Count, PrintDifference);

        public override string DisplayName => Guild.Name;
    }
}
