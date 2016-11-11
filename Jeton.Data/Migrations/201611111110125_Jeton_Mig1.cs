namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jeton_Mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "MachineName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Logs", "Level", c => c.String(maxLength: 100));
            AlterColumn("dbo.Logs", "Logger", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "Logger", c => c.String(maxLength: 50));
            AlterColumn("dbo.Logs", "Level", c => c.String(maxLength: 50));
            DropColumn("dbo.Logs", "MachineName");
        }
    }
}
