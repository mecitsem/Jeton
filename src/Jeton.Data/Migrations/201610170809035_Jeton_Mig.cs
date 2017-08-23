namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jeton_Mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apps",
                c => new
                    {
                        AppID = c.Guid(nullable: false, identity: true),
                        AccessKey = c.String(),
                        Name = c.String(nullable: false, maxLength: 255),
                        IsRoot = c.Boolean(nullable: false),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.AppID);
            
            CreateTable(
                "dbo.Tokes",
                c => new
                    {
                        TokenID = c.Guid(nullable: false, identity: true),
                        TokenKey = c.String(nullable: false),
                        Expire = c.DateTime(),
                        UserID = c.Guid(nullable: false),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.TokenID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        NameId = c.String(nullable: false, maxLength: 255),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokes", "UserID", "dbo.Users");
            DropIndex("dbo.Tokes", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Tokes");
            DropTable("dbo.Apps");
        }
    }
}
