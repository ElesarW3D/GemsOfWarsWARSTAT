namespace GemsOfWarsWARSTAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Defences", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Defences", "Code");
        }
    }
}
