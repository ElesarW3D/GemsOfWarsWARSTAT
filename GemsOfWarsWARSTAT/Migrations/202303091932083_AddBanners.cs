namespace GemsOfWarsWARSTAT.Migrations
{
    using GemsOfWarsWARSTAT.DataContext;
    using System;
    using System.Data.Entity.Migrations;
    using GemsOfWarsMainTypes.Model;
    using GemsOfWarsMainTypes.SubType;
    public partial class AddBanners : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BannerNames",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GameId = c.Int(nullable: false),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Banners",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BannerName_Id = c.Int(),
                    Colors_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BannerNames", t => t.BannerName_Id)
                .ForeignKey("dbo.ColorsCounts", t => t.Colors_Id)
                .Index(t => t.BannerName_Id)
                .Index(t => t.Colors_Id);

            CreateTable(
                "dbo.ColorsCounts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Color = c.Int(nullable: false),
                    Count = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Banners", "Colors_Id", "dbo.ColorsCounts");
            DropForeignKey("dbo.Banners", "BannerName_Id", "dbo.BannerNames");
            DropIndex("dbo.Banners", new[] { "Colors_Id" });
            DropIndex("dbo.Banners", new[] { "BannerName_Id" });
            DropTable("dbo.ColorsCounts");
            DropTable("dbo.Banners");
            DropTable("dbo.BannerNames");
        }
    }
}
