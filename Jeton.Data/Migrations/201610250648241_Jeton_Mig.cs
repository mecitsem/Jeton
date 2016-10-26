namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jeton_Mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingID = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.SettingID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Guid(nullable: false, identity: true),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.SettingValues",
                c => new
                    {
                        SettingRefID = c.Guid(nullable: false),
                        ItemRefID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SettingRefID, t.ItemRefID })
                .ForeignKey("dbo.Settings", t => t.SettingRefID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemRefID, cascadeDelete: true)
                .Index(t => t.SettingRefID)
                .Index(t => t.ItemRefID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SettingValues", "ItemRefID", "dbo.Items");
            DropForeignKey("dbo.SettingValues", "SettingRefID", "dbo.Settings");
            DropIndex("dbo.SettingValues", new[] { "ItemRefID" });
            DropIndex("dbo.SettingValues", new[] { "SettingRefID" });
            DropTable("dbo.SettingValues");
            DropTable("dbo.Items");
            DropTable("dbo.Settings");
        }
    }
}
