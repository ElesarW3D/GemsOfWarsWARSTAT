namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetailRealWarState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealWarStates", "AnabiosAttack", c => c.Int(nullable: false));
            AddColumn("dbo.RealWarStates", "AnabiosLoss", c => c.Int(nullable: false));
            AddColumn("dbo.RealWarStates", "EnemyGuild_Id", c => c.Int());
            CreateIndex("dbo.RealWarStates", "EnemyGuild_Id");
            AddForeignKey("dbo.RealWarStates", "EnemyGuild_Id", "dbo.Guilds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealWarStates", "EnemyGuild_Id", "dbo.Guilds");
            DropIndex("dbo.RealWarStates", new[] { "EnemyGuild_Id" });
            DropColumn("dbo.RealWarStates", "EnemyGuild_Id");
            DropColumn("dbo.RealWarStates", "AnabiosLoss");
            DropColumn("dbo.RealWarStates", "AnabiosAttack");
        }
    }
}
