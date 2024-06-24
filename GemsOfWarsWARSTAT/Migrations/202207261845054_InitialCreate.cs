namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Defences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Units1_Id = c.Int(),
                        Units2_Id = c.Int(),
                        Units3_Id = c.Int(),
                        Units4_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.Units1_Id)
                .ForeignKey("dbo.Units", t => t.Units2_Id)
                .ForeignKey("dbo.Units", t => t.Units3_Id)
                .ForeignKey("dbo.Units", t => t.Units4_Id)
                .Index(t => t.Units1_Id)
                .Index(t => t.Units2_Id)
                .Index(t => t.Units3_Id)
                .Index(t => t.Units4_Id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ColorUnits = c.Int(nullable: false),
                        HeroClassId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HeroClasses", t => t.HeroClassId, cascadeDelete: true)
                .Index(t => t.HeroClassId);
            
            CreateTable(
                "dbo.HeroClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WarDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColorDay = c.Int(nullable: false),
                        DefenceId = c.Int(),
                        WarId = c.Int(),
                        Losses = c.Int(nullable: false),
                        Victories = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Defences", t => t.DefenceId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.WarDays", t => t.WarId)
                .ForeignKey("dbo.Wars", t => t.WarId)
                .Index(t => t.DefenceId)
                .Index(t => t.WarId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Wars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false),
                        Basket = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarDays", "WarId", "dbo.Wars");
            DropForeignKey("dbo.WarDays", "WarId", "dbo.WarDays");
            DropForeignKey("dbo.WarDays", "User_Id", "dbo.Users");
            DropForeignKey("dbo.WarDays", "DefenceId", "dbo.Defences");
            DropForeignKey("dbo.Defences", "Units4_Id", "dbo.Units");
            DropForeignKey("dbo.Defences", "Units3_Id", "dbo.Units");
            DropForeignKey("dbo.Defences", "Units2_Id", "dbo.Units");
            DropForeignKey("dbo.Defences", "Units1_Id", "dbo.Units");
            DropForeignKey("dbo.Units", "HeroClassId", "dbo.HeroClasses");
            DropIndex("dbo.WarDays", new[] { "User_Id" });
            DropIndex("dbo.WarDays", new[] { "WarId" });
            DropIndex("dbo.WarDays", new[] { "DefenceId" });
            DropIndex("dbo.Units", new[] { "HeroClassId" });
            DropIndex("dbo.Defences", new[] { "Units4_Id" });
            DropIndex("dbo.Defences", new[] { "Units3_Id" });
            DropIndex("dbo.Defences", new[] { "Units2_Id" });
            DropIndex("dbo.Defences", new[] { "Units1_Id" });
            DropTable("dbo.Wars");
            DropTable("dbo.WarDays");
            DropTable("dbo.Users");
            DropTable("dbo.HeroClasses");
            DropTable("dbo.Units");
            DropTable("dbo.Defences");
        }
    }
}
