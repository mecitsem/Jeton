namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JetonMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apps",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AccessKey = c.String(maxLength: 4000),
                        Name = c.String(nullable: false, maxLength: 255),
                        IsRoot = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tokes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        TokenKey = c.String(nullable: false, maxLength: 4000),
                        Expire = c.DateTime(),
                        UserId = c.Guid(nullable: false),
                        AppId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apps", t => t.AppId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AppId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        NameId = c.String(nullable: false, maxLength: 255),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Application = c.String(maxLength: 200),
                        Machine = c.String(maxLength: 100),
                        RequestIpAddress = c.String(maxLength: 50),
                        RequestContentType = c.String(),
                        RequestContentBody = c.String(),
                        RequestUri = c.String(),
                        RequestMethod = c.String(maxLength: 50),
                        RequestRouteTemplate = c.String(),
                        RequestRouteData = c.String(),
                        RequestHeaders = c.String(),
                        RequestTimestamp = c.DateTime(nullable: false),
                        ResponseContentType = c.String(maxLength: 100),
                        ResponseContentBody = c.String(),
                        ResponseStatusCode = c.Int(nullable: false),
                        ResponseHeaders = c.String(),
                        ResponseTimestamp = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Value = c.String(),
                        ValueType = c.Int(nullable: false),
                        IsEssential = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 500),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tokes", "AppId", "dbo.Apps");
            DropIndex("dbo.Tokes", new[] { "AppId" });
            DropIndex("dbo.Tokes", new[] { "UserId" });
            DropTable("dbo.Settings");
            DropTable("dbo.Logs");
            DropTable("dbo.Users");
            DropTable("dbo.Tokes");
            DropTable("dbo.Apps");
        }
    }
}
