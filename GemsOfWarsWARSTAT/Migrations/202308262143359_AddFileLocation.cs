namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Guild_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guilds", t => t.Guild_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Guild_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.MultipleFileLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileLocation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileLocations", t => t.FileLocation_Id)
                .Index(t => t.FileLocation_Id);
            
            AddColumn("dbo.WarDays", "MultipleFileLocations_Id", c => c.Int());
            CreateIndex("dbo.WarDays", "MultipleFileLocations_Id");
            AddForeignKey("dbo.WarDays", "MultipleFileLocations_Id", "dbo.MultipleFileLocations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MultipleFileLocations", "FileLocation_Id", "dbo.FileLocations");
            DropForeignKey("dbo.WarDays", "MultipleFileLocations_Id", "dbo.MultipleFileLocations");
            DropForeignKey("dbo.FileLocations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FileLocations", "Guild_Id", "dbo.Guilds");
            DropIndex("dbo.WarDays", new[] { "MultipleFileLocations_Id" });
            DropIndex("dbo.MultipleFileLocations", new[] { "FileLocation_Id" });
            DropIndex("dbo.FileLocations", new[] { "User_Id" });
            DropIndex("dbo.FileLocations", new[] { "Guild_Id" });
            DropColumn("dbo.WarDays", "MultipleFileLocations_Id");
            DropTable("dbo.MultipleFileLocations");
            DropTable("dbo.FileLocations");
        }
    }
}
