namespace eljur_notifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOldPupilId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pupils", "PupilIdOld", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pupils", "PupilIdOld");
        }
    }
}
