namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JetonMig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Apps", "AccessKey", c => c.String());
            AlterColumn("dbo.Apps", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Tokes", "TokenKey", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tokes", "TokenKey", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Apps", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Apps", "AccessKey", c => c.String(maxLength: 255));
        }
    }
}
