namespace eljur_notifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pupils",
                c => new
                    {
                        PupilId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        FullFIO = c.String(),
                        Class = c.String(),
                    })
                .PrimaryKey(t => t.PupilId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pupils");
        }
    }
}
