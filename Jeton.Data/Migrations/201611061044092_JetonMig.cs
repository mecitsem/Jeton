namespace Jeton.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JetonMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingID = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Value = c.String(),
                        ValueType = c.Int(nullable: false),
                        IsEssential = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.SettingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}
