namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenerateDefenceCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "GameId", c => c.Int(nullable: false));
            AddColumn("dbo.HeroClasses", "GameId", c => c.Int(nullable: false));
            AddColumn("dbo.HeroClasses", "GameTrait", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HeroClasses", "GameTrait");
            DropColumn("dbo.HeroClasses", "GameId");
            DropColumn("dbo.Units", "GameId");
        }
    }
}
