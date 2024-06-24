namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWarDayMap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WarColorMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColorDay1 = c.Int(nullable: false),
                        ColorDay2 = c.Int(nullable: false),
                        ColorDay3 = c.Int(nullable: false),
                        ColorDay4 = c.Int(nullable: false),
                        ColorDay5 = c.Int(nullable: false),
                        ColorDay6 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Wars", "MapColor_Id", c => c.Int());
            CreateIndex("dbo.Wars", "MapColor_Id");
            AddForeignKey("dbo.Wars", "MapColor_Id", "dbo.WarColorMaps", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wars", "MapColor_Id", "dbo.WarColorMaps");
            DropIndex("dbo.Wars", new[] { "MapColor_Id" });
            DropColumn("dbo.Wars", "MapColor_Id");
            DropTable("dbo.WarColorMaps");
        }
    }
}
