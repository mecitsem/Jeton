namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jeton_Mig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "IsEssential", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "IsEssential");
        }
    }
}
