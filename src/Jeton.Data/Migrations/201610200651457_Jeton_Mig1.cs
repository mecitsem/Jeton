namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jeton_Mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tokes", "AppID", c => c.Guid(nullable: true));
            CreateIndex("dbo.Tokes", "AppID");
            AddForeignKey("dbo.Tokes", "AppID", "dbo.Apps", "AppID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokes", "AppID", "dbo.Apps");
            DropIndex("dbo.Tokes", new[] { "AppID" });
            DropColumn("dbo.Tokes", "AppID");
        }
    }
}
