
namespace GemsOfWarsMainTypes.Model
{
    public class HeroClass : WarStatsModelViewModel, IExchange<HeroClass>
    {
        public string Name { get; set; }

        public int GameId { get; set; }
        public string GameTrait { get; set; }

        public override object Clone()
        {
            return PersoneClone<HeroClass>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is HeroClass нeroClass &&
                нeroClass.Name == Name &&
                нeroClass.GameId == GameId &&
                нeroClass.GameTrait == GameTrait;
        }

        public void ReadFromItem(HeroClass item)
        {
            Id = item.Id;
            Name = item.Name;
            GameId = item.GameId;
            GameTrait = item.GameTrait;
        }

        public void WriteToItem(HeroClass item)
        {
            item.Id = Id;
            item.Name = Name;
            item.GameId = GameId;
            item.GameTrait = GameTrait;
        }
    }
}
