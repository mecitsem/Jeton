namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jeton_Mig5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "Description");
        }
    }
}
