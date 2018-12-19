namespace eljur_notifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_struct2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        PupilId = c.Int(nullable: false),
                        EventName = c.String(maxLength: 500),
                        NotifyEnable = c.Boolean(nullable: false),
                        NotifyEnableDirector = c.Boolean(nullable: false),
                        NotifyWasSend = c.Boolean(nullable: false),
                        NotifyWasSendDirector = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            DropColumn("dbo.Pupils", "Event");
            DropColumn("dbo.Pupils", "NotifyEnable");
            DropColumn("dbo.Pupils", "NotifyEnableDirector");
            DropColumn("dbo.Pupils", "NotifyWasSend");
            DropColumn("dbo.Pupils", "NotifyWasSendDirector");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pupils", "NotifyWasSendDirector", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupils", "NotifyWasSend", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupils", "NotifyEnableDirector", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupils", "NotifyEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupils", "Event", c => c.String(maxLength: 500));
            DropTable("dbo.Events");
        }
    }
}
