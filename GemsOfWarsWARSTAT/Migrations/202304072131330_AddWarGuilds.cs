namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWarGuilds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WarGuilds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Basket = c.Int(nullable: false),
                        Guild_Id = c.Int(),
                        War_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guilds", t => t.Guild_Id)
                .ForeignKey("dbo.Wars", t => t.War_Id)
                .Index(t => t.Guild_Id)
                .Index(t => t.War_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarGuilds", "War_Id", "dbo.Wars");
            DropForeignKey("dbo.WarGuilds", "Guild_Id", "dbo.Guilds");
            DropIndex("dbo.WarGuilds", new[] { "War_Id" });
            DropIndex("dbo.WarGuilds", new[] { "Guild_Id" });
            DropTable("dbo.WarGuilds");
        }
    }
}
