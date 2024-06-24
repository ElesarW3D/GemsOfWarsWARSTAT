using GemsOfWarsMainTypes.Extension;

namespace GemsOfWarAnalitics
{
    public class CalcStat
    {
        public CalcStat(int victories, int losses, double winRate, double losRate, int count)
        {
            Victories = victories;
            Losses = losses;
            WinRate = winRate;
            LosRate = losRate;
            Count = count;
        }

        public CalcStat Difference { get; set; }
        public int Victories { get;  }
        public int Losses { get; }
        public double WinRate { get; }
        public double LosRate { get; }

        public int Count { get; }

        public string SpecificCountName  => Count.GetName();

    }
}
