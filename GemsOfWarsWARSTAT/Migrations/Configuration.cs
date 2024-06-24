namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GemsOfWarsWARSTAT.DataContext.WarDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GemsOfWarsWARSTAT.DataContext.WarDbContext";
        }

        protected override void Seed(GemsOfWarsWARSTAT.DataContext.WarDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
