namespace eljur_notifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delEnables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pupils", "NotifyEnable", c => c.Boolean(nullable: false));
            DropColumn("dbo.Events", "NotifyEnable");
            DropColumn("dbo.Events", "NotifyEnableDirector");
            DropColumn("dbo.Events", "NotifyWasSendDirector");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "NotifyWasSendDirector", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "NotifyEnableDirector", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "NotifyEnable", c => c.Boolean(nullable: false));
            DropColumn("dbo.Pupils", "NotifyEnable");
        }
    }
}
