using GemsOfWarsMainTypes.Model;
using System.Data.Entity;

namespace GemsOfWarsWARSTAT.DataContext
{
    class DropCreateInitializer : DropCreateDatabaseIfModelChanges<WarDbContext>
    {
        protected override void Seed(WarDbContext db)
        {
            var classes = new string[]
                {
                    "Geomancer",
                    "Elementalist",
                    "Slayer",
                    "Doomsayer",
                    "Monk",
                    "Archmagus",
                    "Sentinel",
                    "Tidecaller",
                    "Corsair",
                    "Oracle",
                    "Mechanist",
                    "Warlord",
                    "Frostmage",
                    "Necromancer",
                    "Orbweaver",
                    "Hierophant",
                    "Archer",
                    "Dervish",
                    "Bard",
                    "Runepriest",
                    "Priest",
                    "Barbarian",
                    "Stormcaller",
                    "Dragonguard",
                    "Sunspear",
                    "Knight",
                    "Titan",
                    "Assassin",
                    "Sorcerer",
                    "Diabolist",
                    "Shaman",
                    "Thief",
                    "Deathknight",
                    "Plaguelord",
                    "Warpriest",
                    "Warden"
                };

            foreach (var className in classes)
            {
                HeroClass heroClass = new HeroClass()
                {
                    Name = className,
                };
                db.HeroClasses.Add(heroClass);
            }
            db.SaveChanges();
        }
    }
}
