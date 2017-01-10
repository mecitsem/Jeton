namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigFirst1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Application", c => c.String(maxLength: 200));
            AddColumn("dbo.Logs", "Machine", c => c.String(maxLength: 100));
            AddColumn("dbo.Logs", "RequestIpAddress", c => c.String(maxLength: 50));
            AddColumn("dbo.Logs", "RequestContentType", c => c.String());
            AddColumn("dbo.Logs", "RequestContentBody", c => c.String());
            AddColumn("dbo.Logs", "RequestUri", c => c.String());
            AddColumn("dbo.Logs", "RequestMethod", c => c.String(maxLength: 50));
            AddColumn("dbo.Logs", "RequestRouteTemplate", c => c.String());
            AddColumn("dbo.Logs", "RequestRouteData", c => c.String());
            AddColumn("dbo.Logs", "RequestHeaders", c => c.String());
            AddColumn("dbo.Logs", "RequestTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.Logs", "ResponseContentType", c => c.String(maxLength: 100));
            AddColumn("dbo.Logs", "ResponseContentBody", c => c.String());
            AddColumn("dbo.Logs", "ResponseStatusCode", c => c.Int(nullable: false));
            AddColumn("dbo.Logs", "ResponseHeaders", c => c.String());
            AddColumn("dbo.Logs", "ResponseTimestamp", c => c.DateTime(nullable: false));
            DropColumn("dbo.Logs", "Level");
            DropColumn("dbo.Logs", "Logger");
            DropColumn("dbo.Logs", "Message");
            DropColumn("dbo.Logs", "MachineName");
            DropColumn("dbo.Logs", "TimeStamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "TimeStamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.Logs", "MachineName", c => c.String(maxLength: 100));
            AddColumn("dbo.Logs", "Message", c => c.String());
            AddColumn("dbo.Logs", "Logger", c => c.String(maxLength: 100));
            AddColumn("dbo.Logs", "Level", c => c.String(maxLength: 100));
            DropColumn("dbo.Logs", "ResponseTimestamp");
            DropColumn("dbo.Logs", "ResponseHeaders");
            DropColumn("dbo.Logs", "ResponseStatusCode");
            DropColumn("dbo.Logs", "ResponseContentBody");
            DropColumn("dbo.Logs", "ResponseContentType");
            DropColumn("dbo.Logs", "RequestTimestamp");
            DropColumn("dbo.Logs", "RequestHeaders");
            DropColumn("dbo.Logs", "RequestRouteData");
            DropColumn("dbo.Logs", "RequestRouteTemplate");
            DropColumn("dbo.Logs", "RequestMethod");
            DropColumn("dbo.Logs", "RequestUri");
            DropColumn("dbo.Logs", "RequestContentBody");
            DropColumn("dbo.Logs", "RequestContentType");
            DropColumn("dbo.Logs", "RequestIpAddress");
            DropColumn("dbo.Logs", "Machine");
            DropColumn("dbo.Logs", "Application");
        }
    }
}
