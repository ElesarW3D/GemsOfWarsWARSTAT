namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectFileLocation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WarDays", "MultipleFileLocations_Id", "dbo.MultipleFileLocations");
            DropIndex("dbo.WarDays", new[] { "MultipleFileLocations_Id" });
            AddColumn("dbo.MultipleFileLocations", "WarDay_Id", c => c.Int());
            CreateIndex("dbo.MultipleFileLocations", "WarDay_Id");
            AddForeignKey("dbo.MultipleFileLocations", "WarDay_Id", "dbo.WarDays", "Id");
            DropColumn("dbo.WarDays", "MultipleFileLocations_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WarDays", "MultipleFileLocations_Id", c => c.Int());
            DropForeignKey("dbo.MultipleFileLocations", "WarDay_Id", "dbo.WarDays");
            DropIndex("dbo.MultipleFileLocations", new[] { "WarDay_Id" });
            DropColumn("dbo.MultipleFileLocations", "WarDay_Id");
            CreateIndex("dbo.WarDays", "MultipleFileLocations_Id");
            AddForeignKey("dbo.WarDays", "MultipleFileLocations_Id", "dbo.MultipleFileLocations", "Id");
        }
    }
}
