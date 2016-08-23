namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JetonMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JetonUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NameId = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GeneratedToken",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        Created = c.DateTime(nullable: false),
                        Expire = c.DateTime(nullable: false),
                        JetonUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JetonUser", t => t.JetonUserId, cascadeDelete: true)
                .Index(t => t.JetonUserId);
            
            CreateTable(
                "dbo.RegisteredApp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessKey = c.String(),
                        AppName = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeneratedToken", "JetonUserId", "dbo.JetonUser");
            DropIndex("dbo.GeneratedToken", new[] { "JetonUserId" });
            DropTable("dbo.RegisteredApp");
            DropTable("dbo.GeneratedToken");
            DropTable("dbo.JetonUser");
        }
    }
}
