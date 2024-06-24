namespace GemsOfWarsWARSTAT.Migrations
{
    using GemsOfWarsMainTypes.Model;
    using GemsOfWarsWARSTAT.DataContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class AddValueInUserInGuild : DbMigration
    {
        public override void Up()
        {
            using (var context = new WarDbContext())
            {
                context.Wars.Load();
                context.Users.Load();
                context.WarDays.Load();
                var anabios = context.Guilds.FirstOrDefault(x => x.Id == 1);
                var lastActualWar = context.Wars.OrderBy(x => x.DateStart).ToArray().Last();
                var users = context.Users.ToArray();
                foreach (var user in users)
                {
                    var warDays = context.WarDays
                        .Where(x => x.User.Id == user.Id)
                        .Select(x => x.War)
                        .Distinct()
                        .OrderBy(x => x.DateStart)
                        .ToArray();
                    if (warDays.Length == 0)
                    {
                        continue;
                    }

                    var warFirst = warDays.First();
                    var warLast = warDays.Last();
                    var userInGuild = context.UsersInGuilds.Add(new UserInGuild()
                    {
                        DateStart = warFirst.DateStart,
                        User = user,
                        Guild = anabios
                    });

                    if (warLast != lastActualWar)
                    {
                        userInGuild.DateFinish = warLast.DateStart.AddSeconds(1);
                    }

                }
                context.SaveChanges();


            }
        }

        public override void Down()
        {
            using (var context = new WarDbContext())
            {
                context.UsersInGuilds.Load();
                var usersInGuild = context.UsersInGuilds.ToArray();
                context.UsersInGuilds.RemoveRange(usersInGuild);
                context.SaveChanges();
            }
        }
    }
}
