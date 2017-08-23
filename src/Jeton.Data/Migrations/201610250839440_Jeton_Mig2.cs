namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jeton_Mig2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SettingValues", "SettingRefID", "dbo.Settings");
            DropForeignKey("dbo.SettingValues", "ItemRefID", "dbo.Items");
            DropIndex("dbo.SettingValues", new[] { "SettingRefID" });
            DropIndex("dbo.SettingValues", new[] { "ItemRefID" });
            AddColumn("dbo.Settings", "Value", c => c.String());
            AddColumn("dbo.Settings", "ValueType", c => c.String(maxLength: 20));
            DropTable("dbo.Items");
            DropTable("dbo.SettingValues");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SettingValues",
                c => new
                    {
                        SettingRefID = c.Guid(nullable: false),
                        ItemRefID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SettingRefID, t.ItemRefID });
            
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
            
            DropColumn("dbo.Settings", "ValueType");
            DropColumn("dbo.Settings", "Value");
            CreateIndex("dbo.SettingValues", "ItemRefID");
            CreateIndex("dbo.SettingValues", "SettingRefID");
            AddForeignKey("dbo.SettingValues", "ItemRefID", "dbo.Items", "ItemID", cascadeDelete: true);
            AddForeignKey("dbo.SettingValues", "SettingRefID", "dbo.Settings", "SettingID", cascadeDelete: true);
        }
    }
}
