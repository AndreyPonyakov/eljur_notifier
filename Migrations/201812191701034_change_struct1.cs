namespace eljur_notifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_struct1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pupils", "Event", c => c.String(maxLength: 500));
            AddColumn("dbo.Pupils", "EljurAccount", c => c.String(maxLength: 500));
            AddColumn("dbo.Pupils", "NotifyEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupils", "NotifyEnableDirector", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupils", "NotifyWasSend", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pupils", "NotifyWasSendDirector", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pupils", "FirstName", c => c.String(maxLength: 500));
            AlterColumn("dbo.Pupils", "LastName", c => c.String(maxLength: 500));
            AlterColumn("dbo.Pupils", "MiddleName", c => c.String(maxLength: 500));
            AlterColumn("dbo.Pupils", "FullFIO", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Pupils", "Class", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pupils", "Class", c => c.String());
            AlterColumn("dbo.Pupils", "FullFIO", c => c.String());
            AlterColumn("dbo.Pupils", "MiddleName", c => c.String());
            AlterColumn("dbo.Pupils", "LastName", c => c.String());
            AlterColumn("dbo.Pupils", "FirstName", c => c.String());
            DropColumn("dbo.Pupils", "NotifyWasSendDirector");
            DropColumn("dbo.Pupils", "NotifyWasSend");
            DropColumn("dbo.Pupils", "NotifyEnableDirector");
            DropColumn("dbo.Pupils", "NotifyEnable");
            DropColumn("dbo.Pupils", "EljurAccount");
            DropColumn("dbo.Pupils", "Event");
        }
    }
}
