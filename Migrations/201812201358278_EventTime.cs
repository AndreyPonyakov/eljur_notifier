namespace eljur_notifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "EventTime");
        }
    }
}
