namespace GemsOfWarAnalitics
{
    public abstract class BaseStatItem
    {
        public abstract string DisplayName { get; }
        public abstract string WinRatePrint { get; }
        public abstract double WinRate { get; }
        public abstract int Count { get; }
    }
}
