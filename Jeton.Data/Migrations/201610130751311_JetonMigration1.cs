namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JetonMigration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tokes", "TokenKey", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tokes", "TokenKey", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
