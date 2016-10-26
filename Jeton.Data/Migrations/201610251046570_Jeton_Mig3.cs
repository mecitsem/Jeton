namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jeton_Mig3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Settings", "ValueType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Settings", "ValueType", c => c.String(maxLength: 20));
        }
    }
}
