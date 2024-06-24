namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserInGuild : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInGuilds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false),
                        DateFinish = c.DateTime(nullable: false),
                        Guild_Id = c.Int(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guilds", t => t.Guild_Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Guild_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInGuilds", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserInGuilds", "Guild_Id", "dbo.Guilds");
            DropIndex("dbo.UserInGuilds", new[] { "User_Id" });
            DropIndex("dbo.UserInGuilds", new[] { "Guild_Id" });
            DropTable("dbo.UserInGuilds");
        }
    }
}
