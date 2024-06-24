namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUsersGuild : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Guild_Id", "dbo.Guilds");
            DropIndex("dbo.Users", new[] { "Guild_Id" });
            DropColumn("dbo.Users", "Guild_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Guild_Id", c => c.Int());
            CreateIndex("dbo.Users", "Guild_Id");
            AddForeignKey("dbo.Users", "Guild_Id", "dbo.Guilds", "Id");
        }
    }
}
