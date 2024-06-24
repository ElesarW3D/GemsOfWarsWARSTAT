using GemsOfWarsMainTypes.Extension;
using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsWARSTAT.DataContext
{
    public class WarDbContext : DbContext
    {
        static WarDbContext()
        {
            Database.SetInitializer(new DropCreateInitializer());
        }
        public WarDbContext()
          : base("WarDbConnection")
        { }

        public static string ConnectionString = "Data Source = (LocalDb)\\MSSQLLocalDB;AttachDbFilename={0};Initial Catalog = YourDatabase; Integrated Security = True";

        public WarDbContext(string fileName)
          : base(ConnectionString.Args(fileName))
        { }

        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WarDay> WarDays { get; set; }
        public DbSet<War> Wars { get; set; }
        public DbSet<WarGuild> WarsGuilds { get; set; }
        public DbSet<Defence> Defences { get; set; }
        public DbSet<HeroClass> HeroClasses { get; set; }
        public DbSet<RealWarState> RealWarStates { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<UserInGuild> UsersInGuilds { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ColorsCount> ColorsCounts { get; set; }
        public DbSet<BannerName> BannerNames { get; set; }

        public DbSet<FileLocation> FileLocations { get; set; }

        public DbSet<MultipleFileLocations> MultipleFileLocations { get; set; }

        public DbSet<WarColorMap> WarColorMaps { get; set; }

        public void CopyTo(WarDbContext other)
        {
            HeroClasses.Load();
            other.HeroClasses.AddRange(HeroClasses.Local.ToList());
            Units.Load();
            other.Units.AddRange(Units.Local.ToList());
            

            Guilds.Load();
            other.Guilds.AddRange(Guilds.Local.ToList());
            Users.Load();
            other.Users.AddRange(Users.Local.ToList());

            UsersInGuilds.Include(x => x.Guild).Include(x => x.User).Load();
            other.UsersInGuilds.AddRange(UsersInGuilds.Local.ToList());
            
            
            
            //Context.WarDays.Include(x => x.User).Load();
            //Context.HeroClasses.Load();
            //Context.Defences.Load();

            //Context.Wars.Load();
            //_wars = Context.Wars.OrderBy(x => x.DateStart).ToList();
            //_warGuilds = Context.WarsGuilds.ToList();
            //_baskets = Context.WarsGuilds.Select(x => x.Basket).Distinct().ToArray();

            //Context.Defences
            //    .Include(db => db.Units1)
            //    .Include(db => db.Units2)
            //    .Include(db => db.Units3)
            //    .Include(db => db.Units4)
            //    .Load();

            //_defences = Context.Defences.Local.ToList();

            //Context.WarDays
            //.Include(db => db.Defence)
            //.Include(db => db.Defence.Units1)
            //.Include(db => db.Defence.Units2)
            //.Include(db => db.Defence.Units3)
            //.Include(db => db.Defence.Units4)
            //.Include(db => db.User)
            //.Load();

            //_warDays = Context.WarDays.ToList();

            //other.Users.AddRange(Users);
            //other.WarDays.AddRange(WarDays);


            //other.Defences.AddRange(Defences);
            //other.HeroClasses.AddRange(HeroClasses);
            //other.RealWarStates.AddRange(RealWarStates);
            //other.Guilds.AddRange(Guilds);
            //other.UsersInGuilds.AddRange(UsersInGuilds);

            //other.Wars.AddRange(Wars);
            //other.WarsGuilds.AddRange(WarsGuilds);

        }

    }
}
