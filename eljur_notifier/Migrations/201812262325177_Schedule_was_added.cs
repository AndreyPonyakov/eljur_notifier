namespace eljur_notifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schedule_was_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        Clas = c.String(maxLength: 10),
                        StartTimeLessons = c.Time(nullable: false, precision: 7),
                        EndTimeLessons = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ScheduleId);
            
            AddColumn("dbo.Events", "PupilIdOld", c => c.Int(nullable: false));
            AddColumn("dbo.Pupils", "Clas", c => c.String(maxLength: 10));
            DropColumn("dbo.Events", "PupilId");
            DropColumn("dbo.Pupils", "Class");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pupils", "Class", c => c.String(maxLength: 10));
            AddColumn("dbo.Events", "PupilId", c => c.Int(nullable: false));
            DropColumn("dbo.Pupils", "Clas");
            DropColumn("dbo.Events", "PupilIdOld");
            DropTable("dbo.Schedules");
        }
    }
}
