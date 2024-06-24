namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorWarDayMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WarDays", "DefenceId", "dbo.Defences");
            DropForeignKey("dbo.WarDays", "User_Id", "dbo.Users");
            DropForeignKey("dbo.WarDays", "WarId", "dbo.WarDays");
            DropForeignKey("dbo.WarDays", "WarId", "dbo.Wars");
            DropIndex("dbo.WarDays", new[] { "DefenceId" });
            DropIndex("dbo.WarDays", new[] { "WarId" });
            DropIndex("dbo.WarDays", new[] { "User_Id" });
            RenameColumn(table: "dbo.WarDays", name: "DefenceId", newName: "Defence_Id");
            RenameColumn(table: "dbo.WarDays", name: "WarId", newName: "War_Id");
            AlterColumn("dbo.WarDays", "Defence_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.WarDays", "War_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.WarDays", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.WarDays", "Defence_Id");
            CreateIndex("dbo.WarDays", "User_Id");
            CreateIndex("dbo.WarDays", "War_Id");
            AddForeignKey("dbo.WarDays", "Defence_Id", "dbo.Defences", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WarDays", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WarDays", "War_Id", "dbo.Wars", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarDays", "War_Id", "dbo.Wars");
            DropForeignKey("dbo.WarDays", "User_Id", "dbo.Users");
            DropForeignKey("dbo.WarDays", "Defence_Id", "dbo.Defences");
            DropIndex("dbo.WarDays", new[] { "War_Id" });
            DropIndex("dbo.WarDays", new[] { "User_Id" });
            DropIndex("dbo.WarDays", new[] { "Defence_Id" });
            AlterColumn("dbo.WarDays", "User_Id", c => c.Int());
            AlterColumn("dbo.WarDays", "War_Id", c => c.Int());
            AlterColumn("dbo.WarDays", "Defence_Id", c => c.Int());
            RenameColumn(table: "dbo.WarDays", name: "War_Id", newName: "WarId");
            RenameColumn(table: "dbo.WarDays", name: "Defence_Id", newName: "DefenceId");
            CreateIndex("dbo.WarDays", "User_Id");
            CreateIndex("dbo.WarDays", "WarId");
            CreateIndex("dbo.WarDays", "DefenceId");
            AddForeignKey("dbo.WarDays", "WarId", "dbo.Wars", "Id");
            AddForeignKey("dbo.WarDays", "WarId", "dbo.WarDays", "Id");
            AddForeignKey("dbo.WarDays", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.WarDays", "DefenceId", "dbo.Defences", "Id");
        }
    }
}
