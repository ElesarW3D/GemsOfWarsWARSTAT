using GemsOfWarsMainTypes;
using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;

namespace GemsOfWarAnalitics
{
    public class StatUser : StatItem
    {
        public StatUser(CalcStat statictic, User user, bool useDiff) : base(useDiff, statictic)
        {
            User = user;
        }

        public User User { get; }

        public override string DisValue => GlobalConstants.DisValue.Args(User.Name, Statictic.WinRate, Statictic.SpecificCountName);

        public override string PrintValue => GlobalConstants.PrintValue.Args(User.Name, Statictic.WinRate, Statictic.SpecificCountName);

        public override string DisValueDiff => GlobalConstants.DisValueDiff.Args(User.Name, Statictic.WinRate, Statictic.SpecificCountName, PrintDifference);

        public override string PrintValueDiff => GlobalConstants.PrintValueDiff.Args(User.Name, Statictic.WinRate, Statictic.SpecificCountName, PrintDifference);

        public override string DisplayName => User.Name;
    }
}
