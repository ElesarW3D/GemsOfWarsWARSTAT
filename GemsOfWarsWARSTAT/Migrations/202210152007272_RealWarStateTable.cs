namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RealWarStateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RealWarStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColorDay = c.Int(nullable: false),
                        CountAttack = c.Int(nullable: false),
                        CountLossEnemy = c.Int(nullable: false),
                        War_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wars", t => t.War_Id)
                .Index(t => t.War_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealWarStates", "War_Id", "dbo.Wars");
            DropIndex("dbo.RealWarStates", new[] { "War_Id" });
            DropTable("dbo.RealWarStates");
        }
    }
}
