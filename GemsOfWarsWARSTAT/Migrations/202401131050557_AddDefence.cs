namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Defences", "HeroClass_Id", c => c.Int());
            CreateIndex("dbo.Defences", "HeroClass_Id");
            AddForeignKey("dbo.Defences", "HeroClass_Id", "dbo.HeroClasses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Defences", "HeroClass_Id", "dbo.HeroClasses");
            DropIndex("dbo.Defences", new[] { "HeroClass_Id" });
            DropColumn("dbo.Defences", "HeroClass_Id");
        }
    }
}
