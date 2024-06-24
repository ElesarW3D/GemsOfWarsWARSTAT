namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypedateFinish : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInGuilds", "DateFinish", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInGuilds", "DateFinish", c => c.DateTime(nullable: false));
        }
    }
}
