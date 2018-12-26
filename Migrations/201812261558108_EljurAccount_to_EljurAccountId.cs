namespace eljur_notifier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EljurAccount_to_EljurAccountId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pupils", "EljurAccountId", c => c.Int(nullable: false));
            DropColumn("dbo.Pupils", "EljurAccount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pupils", "EljurAccount", c => c.String(maxLength: 500));
            DropColumn("dbo.Pupils", "EljurAccountId");
        }
    }
}
