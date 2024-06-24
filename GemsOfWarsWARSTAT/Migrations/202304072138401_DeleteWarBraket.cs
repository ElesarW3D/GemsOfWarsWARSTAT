namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteWarBraket : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Wars", "Basket");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wars", "Basket", c => c.Int(nullable: false));
        }
    }
}
