namespace GemsOfWarsWARSTAT.Migrations
{
    using GemsOfWarsMainTypes.Model;
    using GemsOfWarsWARSTAT.DataContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class AddDataToWarGuild : DbMigration
    {
        public override void Up()
        {
            //Вставка данных в новую таблицу
            //using (var context = new WarDbContext())
            //{
            //    context.Wars.Load();
            //    var oldWars = context.Wars.ToArray();
            //    var anabios = context.Guilds.FirstOrDefault(x => x.Id == 1);
            //    foreach (var oldWar in oldWars)
            //    {
            //        context.WarsGuilds.Add(new WarGuild() { War = oldWar, Basket = oldWar.Basket, Guild = anabios });
            //    }
            //    context.SaveChanges();
            //}
        }

        public override void Down()
        {
        }
    }
}
