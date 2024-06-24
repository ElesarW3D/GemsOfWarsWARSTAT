namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuild : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guilds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Guild_Id", c => c.Int());
            CreateIndex("dbo.Users", "Guild_Id");
            AddForeignKey("dbo.Users", "Guild_Id", "dbo.Guilds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Guild_Id", "dbo.Guilds");
            DropIndex("dbo.Users", new[] { "Guild_Id" });
            DropColumn("dbo.Users", "Guild_Id");
            DropTable("dbo.Guilds");
        }
    }
}
