namespace GemsOfWarsWARSTAT.Migrations
{
    using GemsOfWarsMainTypes.Model;
    using GemsOfWarsWARSTAT.DataContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class CloneHeroClass : DbMigration
    {
        public override void Up()
        {
            using (var context = new WarDbContext())
            {
                context.Defences.Load();
                context.Defences
                    .Include(db => db.Units1)
                    .Include(db => db.Units2)
                    .Include(db => db.Units3)
                    .Include(db => db.Units4)
                    .Load();
                context.Units.Load();
                context.HeroClasses.Load();
                var defences = context.Defences.Local.ToArray();
                foreach (var def in defences)
                {
                    foreach (var item in def.GetUnits())
                    {
                        if (item is HeroWeapon weapon)
                        {
                            def.HeroClass = weapon.HeroClass;
                            break;
                        }
                    }
                }
                context.SaveChanges();
            }
        }
        
        public override void Down()
        {
        }
    }
}
