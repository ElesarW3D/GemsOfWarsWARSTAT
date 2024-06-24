namespace GemsOfWarsWARSTAT.Migrations
{
    using GemsOfWarsMainTypes.Model;
    using GemsOfWarsWARSTAT.DataContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class CopyUnit : DbMigration
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
                context.HeroClasses.Load();
                context.Units.Load();
                var weapons = context.Units.Local.Where(x => x is HeroWeapon).ToArray();
                

                var weaponsGroups = weapons.GroupBy(x => x.GameId);

                foreach (var weaponGroup in weaponsGroups)
                {
                    var firstGroup = weaponGroup.First();
                    var newWeapon = new Unit();
                    newWeapon.ReadFromItem(firstGroup);
                    newWeapon = context.Units.Add(newWeapon);
                    var defences = context.Defences.Local
                        .Where((x) => weaponGroup.Contains(x.Units1)
                                    || weaponGroup.Contains(x.Units2)
                                    || weaponGroup.Contains(x.Units3)
                                    || weaponGroup.Contains(x.Units4)).ToArray();

                    foreach (var def in defences)
                    {
                        if (weaponGroup.Contains(def.Units1 ))
                        {
                            def.Units1 = newWeapon;
                        }
                        if (weaponGroup.Contains(def.Units2))
                        {
                            def.Units2 = newWeapon;
                        }
                        if (weaponGroup.Contains(def.Units3))
                        {
                            def.Units3 = newWeapon;
                        }
                        if (weaponGroup.Contains(def.Units4))
                        {
                            def.Units4 = newWeapon;
                        }
                    }
                    foreach (var oldWeapon in weaponGroup)
                    {
                        context.Units.Remove(oldWeapon);
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
